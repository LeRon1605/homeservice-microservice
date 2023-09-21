using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Serilog;
using Shopping.API.Extensions;
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

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

// var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();

await app.ApplyMigrationAsync<OrderDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();