using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Swagger;
using IAC.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "IdentityService");

builder.Services.AddSwagger("IdentityService")
                // .AddServiceCache()
                .AddRepositories()
                .AddMapper()
                .AddServices()
                .AddApplicationExceptionHandler()
                .AddEventBus(builder.Configuration)
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddIdentity(builder.Configuration, builder.Environment);

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseApplicationExceptionHandler();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.ApplyMigrationAsync(app.Logger);
await app.SeedDataAsync();

app.Run();