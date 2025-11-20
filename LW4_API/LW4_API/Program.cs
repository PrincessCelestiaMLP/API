using FluentValidation;
using FluentValidation.AspNetCore;
using LW4_API.Data;
using LW4_API.FluentValidator;
using LW4_API.Model;
using LW4_API.Repository.Interface;
using LW4_API.Repository.Realization;
using LW4_API.Server.Realizetion;
using LW4_API.Mapper;
using LW4_API.Server.Interface;
using LW4_API.Server;
using LW4_API.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ================== MongoDB ==================
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient("mongodb+srv://nazarshejkin_db_user:eXsanO17aQW49eV6@nazar.pjrmold.mongodb.net/?appName=Nazar"));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("mydatabase"));

// ================== AutoMapper ==================
builder.Services.AddAutoMapper(typeof(ClientProfile), typeof(RentProfile), typeof(ParkingSpaceProfile));

// ================== Repositories ==================
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

// ================== Services ==================
builder.Services.AddScoped<IClientService, ClientServer>();
builder.Services.AddScoped<IParkingService, ParkingSpaceService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

// ================== Controllers + FluentValidation ==================
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ParckingValidator>();

// ================== JWT Settings ==================
var jwtSection = builder.Configuration.GetSection("JWT");
var jwtSettings = jwtSection.Get<JwtSettings>() ?? throw new Exception("JWT section is missing!");

// Прибираємо лапки, якщо Railway їх автоматично додає
jwtSettings.SecretKey = jwtSettings.SecretKey?.Trim('"') ?? throw new Exception("JWT SecretKey is null or empty!");

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddSingleton<JwtTokenGenerator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

builder.Services.AddAuthorization();

// ================== Swagger ==================
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Введіть токен у форматі: Bearer {токен}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ================== Kestrel Port (Railway) ==================
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

var app = builder.Build();

// ================== Middleware ==================
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
