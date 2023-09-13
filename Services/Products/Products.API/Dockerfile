FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Products/Products.API/Products.API.csproj", "Services/Products/Products.API/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["Services/Products/Products.Application/Products.Application.csproj", "Services/Products/Products.Application/"]
COPY ["Services/Products/Products.Domain/Products.Domain.csproj", "Services/Products/Products.Domain/"]
COPY ["Services/Products/Products.Infrastructure.EfCore/Products.Infrastructure.EfCore.csproj", "Services/Products/Products.Infrastructure.EfCore/"]
RUN dotnet restore "Services/Products/Products.API/Products.API.csproj"
COPY . .
WORKDIR "/src/Services/Products/Products.API"
RUN dotnet build "Products.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Products.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Products.API.dll"]
