using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Customers.API.Extensions;
using Customers.Application.IntegrationEvents.Events;
using Customers.Application.IntegrationEvents.Handlers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "CustomerService");

builder.Services.AddSwagger("CustomerService")
                .AddEventBus(builder.Configuration)
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddRepositories()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddApplicationExceptionHandler()
                .AddCqrs()
                .AddValidators()
                .AddMapper()
                .AddService()
                .AddSeeder();

builder.Services.AddControllers();
builder.Services
    .AddScoped<IIntegrationEventHandler<UserSignedUpIntegrationEvent>, UserSignedUpIntegrationEventHandler>();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<UserSignedUpIntegrationEvent, IIntegrationEventHandler<UserSignedUpIntegrationEvent>>();


app.UseApplicationExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseAuthentication();

await app.ApplyMigrationAsync(app.Logger);
await app.SeedDataAsync();

app.Run();