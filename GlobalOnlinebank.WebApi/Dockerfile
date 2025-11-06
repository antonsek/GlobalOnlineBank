FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GlobalOnlinebank.WebApi/GlobalOnlinebank.WebApi.csproj", "GlobalOnlinebank.WebApi/"]
COPY ["GlobalOnlinebank.Application/GlobalOnlinebank.Application.csproj", "GlobalOnlinebank.Application/"]
COPY ["GlobalOnlinebank.Domain/GlobalOnlinebank.Domain.csproj", "GlobalOnlinebank.Domain/"]
COPY ["GlobalOnlinebank.Infrastructure/GlobalOnlinebank.Infrastructure.csproj", "GlobalOnlinebank.Infrastructure/"]
RUN dotnet restore "GlobalOnlinebank.WebApi/GlobalOnlinebank.WebApi.csproj"
COPY . .
WORKDIR "/src/GlobalOnlinebank.WebApi"
RUN dotnet build "./GlobalOnlinebank.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GlobalOnlinebank.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlobalOnlinebank.WebApi.dll"]
