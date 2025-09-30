FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Dominio/Dominio.csproj", "src/Dominio/"]
COPY ["src/Infraestrutura/Infraestrutura.csproj", "src/Infraestrutura/"]
COPY ["src/Aplicacao/Aplicacao.csproj", "src/Aplicacao/"]
RUN dotnet restore "./src/Api/Api.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet publish "./Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated9.0
WORKDIR /home/site/wwwroot
COPY --from=build /app/publish .