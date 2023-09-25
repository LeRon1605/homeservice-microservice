using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Serilog;
using Shopping.API.Extensions;
using Shopping.Application.IntegrationEvents.EventHandling;
using Shopping.Application.IntegrationEvents.Events;
using Shopping.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ShoppingService");

builder.Services.AddSwagger("ShoppingService")
                .AddEfCoreDbContext<OrderDbContext>(builder.Configuration)
                .AddEventBus(builder.Configuration)
                .AddRepositories()
                .AddMapper()
                .AddCqrs()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddCurrentUser();
builder.Services
    .AddScoped<IIntegrationEventHandler<ProductAddedIntegrationEvent>, ProductAddedIntegrationEventHandler>();
builder.Services
    .AddScoped<IIntegrationEventHandler<ProductUpdatedIntegrationEvent>, ProductUpdatedIntegrationEventHandler>();
builder.Services
    .AddScoped<IIntegrationEventHandler<ProductDeletedIntegrationEvent>, ProductDeletedIntegrationEventHandler>();
builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<ProductAddedIntegrationEvent, IIntegrationEventHandler<ProductAddedIntegrationEvent>>();
eventBus.Subscribe<ProductUpdatedIntegrationEvent, IIntegrationEventHandler<ProductUpdatedIntegrationEvent>>();
eventBus.Subscribe<ProductDeletedIntegrationEvent, IIntegrationEventHandler<ProductDeletedIntegrationEvent>>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();

await app.ApplyMigrationAsync<OrderDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();