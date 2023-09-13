FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Customers/Customers.API/Customers.API.csproj", "Services/Customers/Customers.API/"]
COPY ["Services/Customers/Customers.Infrastructure.EfCore/Customers.Infrastructure.EfCore.csproj", "Services/Customers/Customers.Infrastructure.EfCore/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["Services/Customers/Customers.Domain/Customers.Domain.csproj", "Services/Customers/Customers.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
COPY ["Services/Customers/Customers.Application/Customers.Application.csproj", "Services/Customers/Customers.Application/"]
RUN dotnet restore "Services/Customers/Customers.API/Customers.API.csproj"
COPY . .
WORKDIR "/src/Services/Customers/Customers.API"
RUN dotnet build "Customers.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customers.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.API.dll"]
