# Usa la imagen base de .NET Core 8 Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen de SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo del proyecto y restaura las dependencias
COPY ["BasicSupermarket.csproj", "./"]
RUN dotnet restore "./BasicSupermarket.csproj"

# Copia el resto del código fuente
COPY . .

# Construye la aplicación
RUN dotnet build "BasicSupermarket.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "BasicSupermarket.csproj" -c Release -o /app/publish

# Copia los archivos publicados a la imagen base
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasicSupermarket.dll"]