using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Customers.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "CustomerService");

builder.Services.AddSwagger("CustomerService")
                .AddEventBus(builder.Configuration)
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddRepositories()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddCqrs()
                .AddMapper()
                .AddService()
                .AddSeeder();

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseAuthentication();

await app.ApplyMigrationAsync(app.Logger);
await app.SeedDataAsync();

app.Run();