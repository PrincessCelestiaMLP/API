# ====== BUILD STAGE ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо csproj окремо — це кешує restore
COPY *.csproj ./
RUN dotnet restore

# Копіюємо весь проєкт
COPY . ./

# Публікуємо в Release
RUN dotnet publish -c Release -o /app


# ====== RUNTIME STAGE ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копіюємо з build stage
COPY --from=build /app .

# Railway працює на порту 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Запуск
ENTRYPOINT ["dotnet", "LW4_API.dll"]
