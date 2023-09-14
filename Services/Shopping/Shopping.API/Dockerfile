FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Shopping/Shopping.API/Shopping.API.csproj", "Services/Shopping/Shopping.API/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
COPY ["Services/Shopping/Shopping.Application/Shopping.Application.csproj", "Services/Shopping/Shopping.Application/"]
COPY ["Services/Shopping/Shopping.Domain/Shopping.Domain.csproj", "Services/Shopping/Shopping.Domain/"]
COPY ["Services/Shopping/Shopping.Infrastructure.EfCore/Shopping.Infrastructure.EfCore.csproj", "Services/Shopping/Shopping.Infrastructure.EfCore/"]
RUN dotnet restore "Services/Shopping/Shopping.API/Shopping.API.csproj"
COPY . .
WORKDIR "/src/Services/Shopping/Shopping.API"
RUN dotnet build "Shopping.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.API.dll"]
