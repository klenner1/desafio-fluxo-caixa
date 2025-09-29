
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Dominio/Dominio.csproj", "src/Dominio/"]
COPY ["src/Infraestrutura/Infraestrutura.csproj", "src/Infraestrutura/"]
COPY ["src/Aplicacao/Aplicacao.csproj", "src/Aplicacao/"]
RUN dotnet restore "./src/Api/Api.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]