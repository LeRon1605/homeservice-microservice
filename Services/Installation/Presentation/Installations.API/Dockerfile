FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Installation/Presentation/Installations.API/Installations.API.csproj", "Services/Installation/Presentation/Installations.API/"]
COPY ["Services/Installation/Core/Installations.Domain/Installations.Domain.csproj", "Services/Installation/Core/Installations.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["Services/Installation/Infrastructure/Installations.Infrastructure/Installations.Infrastructure.csproj", "Services/Installation/Infrastructure/Installations.Infrastructure/"]
COPY ["Services/Installation/Core/Installations.Application/Installations.Application.csproj", "Services/Installation/Core/Installations.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
RUN dotnet restore "Services/Installation/Presentation/Installations.API/Installations.API.csproj"
COPY . .
WORKDIR "/src/Services/Installation/Presentation/Installations.API"
RUN dotnet build "Installations.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Installations.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Installations.API.dll"]
