FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Employees/Presentation/Employees.API/Employees.API.csproj", "Services/Employees/Presentation/Employees.API/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["Services/Employees/Core/Employees.Application/Employees.Application.csproj", "Services/Employees/Core/Employees.Application/"]
COPY ["Services/Employees/Core/Employees.Domain/Employees.Domain.csproj", "Services/Employees/Core/Employees.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["Services/Employees/Infrastructure/Employees.Infrastructure/Employees.Infrastructure.csproj", "Services/Employees/Infrastructure/Employees.Infrastructure/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
RUN dotnet restore "Services/Employees/Presentation/Employees.API/Employees.API.csproj"
COPY . .
WORKDIR "/src/Services/Employees/Presentation/Employees.API"
RUN dotnet build "Employees.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Employees.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Employees.API.dll"]
