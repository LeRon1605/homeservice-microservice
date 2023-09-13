FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/IAC/IAC.API/IAC.API.csproj", "Services/IAC/IAC.API/"]
COPY ["BuildingBlocks/BuildingBlocks.Presentation/BuildingBlocks.Presentation.csproj", "BuildingBlocks/BuildingBlocks.Presentation/"]
COPY ["BuildingBlocks/BuildingBlocks.EventBus/BuildingBlocks.EventBus.csproj", "BuildingBlocks/BuildingBlocks.EventBus/"]
COPY ["BuildingBlocks/BuildingBlocks.Application/BuildingBlocks.Application.csproj", "BuildingBlocks/BuildingBlocks.Application/"]
COPY ["BuildingBlocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks/BuildingBlocks.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks/BuildingBlocks.Infrastructure/"]
COPY ["Services/IAC/IAC.Application/IAC.Application.csproj", "Services/IAC/IAC.Application/"]
COPY ["Services/IAC/IAC.Domain/IAC.Domain.csproj", "Services/IAC/IAC.Domain/"]
COPY ["Services/IAC/IAC.Infrastructure.EfCore/IAC.Infrastructure.EfCore.csproj", "Services/IAC/IAC.Infrastructure.EfCore/"]
RUN dotnet restore "Services/IAC/IAC.API/IAC.API.csproj"
COPY . .
WORKDIR "/src/Services/IAC/IAC.API"
RUN dotnet build "IAC.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IAC.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IAC.API.dll"]
