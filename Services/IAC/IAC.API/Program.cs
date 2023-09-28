using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Redis;
using BuildingBlocks.Presentation.Swagger;
using IAC.API.Extensions;
using IAC.Application.Grpc.Services;
using IAC.Application.IntegrationEvents.Events;
using IAC.Application.IntegrationEvents.Handlers;
using IAC.Infrastructure.EfCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "IdentityService");

builder.Services.AddSwagger("IdentityService")
    .AddRepositories()
    .AddMapper()
    .AddServices()
    .AddApplicationExceptionHandler()
    .AddEventBus(builder.Configuration)
    .AddEfCoreDbContext<IacDbContext>(builder.Configuration)
    .AddIdentity(builder.Configuration, builder.Environment)
    .AddApplicationAuthentication(builder.Configuration)
    .AddConfiguration(builder.Configuration)
    .AddCurrentUser()
    .AddRedisDistributedCache(builder.Configuration)
    .AddGrpc();

builder.Services.AddControllers();

builder.Services
    .AddScoped<IIntegrationEventHandler<CustomerInfoChangedIntegrationEvent>, CustomerInfoChangedIntegrationEventHandler>();

builder.Services
    .AddScoped<IIntegrationEventHandler<CustomerDeletedIntegrationEvent>, CustomerDeletedIntegrationEventHandler>();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseApplicationExceptionHandler();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<CustomerInfoChangedIntegrationEvent, IIntegrationEventHandler<CustomerInfoChangedIntegrationEvent>>();
eventBus.Subscribe<CustomerDeletedIntegrationEvent, IIntegrationEventHandler<CustomerDeletedIntegrationEvent>>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<AuthGrpcService>();
app.MapControllers();

await app.ApplyMigrationAsync<IacDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();