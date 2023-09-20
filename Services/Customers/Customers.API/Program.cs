using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Customers.API.Extensions;
using Customers.Infrastructure.EfCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "CustomerService");

builder.Services.AddSwagger("CustomerService")
                .AddEventBus(builder.Configuration)
                .AddEfCoreDbContext<CustomerDbContext>(builder.Configuration)
                .AddRepositories()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddApplicationExceptionHandler()
                .AddCqrs()
                .AddValidators()
                .AddMapper()
                .AddCurrentUser()
                .AddSeeder();

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseApplicationExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseAuthentication();

await app.ApplyMigrationAsync<CustomerDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();