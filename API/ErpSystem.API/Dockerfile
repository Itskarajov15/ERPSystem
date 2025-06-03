FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ErpSystem.API/ErpSystem.API.csproj", "ErpSystem.API/"]
COPY ["ErpSystem.Application/ErpSystem.Application.csproj", "ErpSystem.Application/"]
COPY ["ErpSystem.Domain/ErpSystem.Domain.csproj", "ErpSystem.Domain/"]
COPY ["ErpSystem.Infrastructure/ErpSystem.Infrastructure.csproj", "ErpSystem.Infrastructure/"]
RUN dotnet restore "./ErpSystem.API/ErpSystem.API.csproj"
COPY . .
WORKDIR "/src/ErpSystem.API"
RUN dotnet build "./ErpSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ErpSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ErpSystem.API.dll"]