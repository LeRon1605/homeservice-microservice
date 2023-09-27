FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Contracts/Contracts.API/Contracts.API.csproj", "Services/Contracts/Contracts.API/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["Services/Contracts/Contracts.Application/Contracts.Application.csproj", "Services/Contracts/Contracts.Application/"]
COPY ["Services/Contracts/Contracts.Domain/Contracts.Domain.csproj", "Services/Contracts/Contracts.Domain/"]
COPY ["Services/Contracts/Contracts.Infrastructure.EfCore/Contracts.Infrastructure.EfCore.csproj", "Services/Contracts/Contracts.Infrastructure.EfCore/"]
RUN dotnet restore "Services/Contracts/Contracts.API/Contracts.API.csproj"
COPY . .
WORKDIR "/src/Services/Contracts/Contracts.API"
RUN dotnet build "Contracts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contracts.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contracts.API.dll"]
