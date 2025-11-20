using FluentValidation;
using FluentValidation.AspNetCore;
using LW4_API.Data;
using LW4_API.FluentValidator;
using LW4_API.Model;
using LW4_API.Repository.Interface;
using LW4_API.Repository.Realization;
using LW4_API.Server.Realizetion;
using MongoDB.Driver;
using LW4_API.Mapper;
using LW4_API.Server.Interface;
using LW4_API.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using LW4_API.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
var builder = WebApplication.CreateBuilder(args);
//==========Підключення бази даних==================
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient("mongodb+srv://nazarshejkin_db_user:eXsanO17aQW49eV6@nazar.pjrmold.mongodb.net/?appName=Nazar"));

builder.Services.AddSingleton<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("mydatabase"));

//==========Підключення маперів==================
builder.Services.AddAutoMapper(typeof(ClientProfile));
builder.Services.AddAutoMapper(typeof(RentProfile));
builder.Services.AddAutoMapper(typeof(ParkingSpaceProfile));
//==========Підключення Репозиторіїв==================
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IParkingSpaceRepository, ParkingSpaceRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();



builder.Services.AddSwaggerGen();

//==========Підключення сервісів==================
builder.Services.AddScoped<IClientService,ClientServer>();
builder.Services.AddScoped<IParkingService,ParkingSpaceService>();
builder.Services.AddScoped<IRentService,RentService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
//==========Підключення контролерів==================
builder.Services.AddControllers()
    .AddFluentValidation();
//==========JWT НАСТРОЙКИ==================
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<JwtTokenGenerator>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
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
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

builder.Services.AddAuthorization();

//==========Swagger з підтримкою JWT==================
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    // Додаємо JWT авторизацію
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Введіть токен у форматі: Bearer {токен}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(Int32.Parse(port));
});
//==========Підключення валідація==================
builder.Services.AddValidatorsFromAssemblyContaining<ClientValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ParckingValidator>();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

    app.UseSwagger();      
    app.UseSwaggerUI();    


app.Run();


