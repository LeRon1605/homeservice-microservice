using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Redis;
using BuildingBlocks.Presentation.Swagger;
using Contracts.API.Extensions;
using Contracts.Infrastructure.EfCore;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ContractService");

builder.Services
    .AddSwagger("ContractService")
    .AddEventBus(builder.Configuration)
    .AddEfCoreDbContext<ContractDbContext>(builder.Configuration)
    .AddRepositories()
    .AddHomeServiceAuthentication(builder.Configuration)
    .AddApplicationExceptionHandler()
    .AddCurrentUser()
    .AddCqrs()
    .AddMapper()
    .AddIntegrationEventHandlers()
    .AddRedisDistributedCache(builder.Configuration)
    .AddValidators();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseEventBus();

app.UseApplicationExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseAuthentication();

await app.ApplyMigrationAsync<ContractDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();