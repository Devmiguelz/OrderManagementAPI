# Usa la imagen base de .NET 6
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usa una imagen de SDK para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar los archivos del proyecto
COPY ["OrderManagementAPI.csproj", "./"]
RUN dotnet restore "OrderManagementAPI.csproj"

# Copia el resto del código fuente
COPY . .
WORKDIR "/src"
RUN dotnet build "OrderManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagementAPI.csproj" -c Release -o /app/publish

# Copia los archivos publicados al contenedor base y configura el entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagementAPI.dll"]
