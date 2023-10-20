using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Installations.API.Extensions;
using Installations.Infrastructure;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "InstallationService");

builder.Services.AddSwagger("InstallationService")
                .AddEfCoreDbContext<InstallationDbContext>(builder.Configuration)
                .AddEventBus(builder.Configuration)
                .AddApplicationExceptionHandler()
                .AddMapper()
                .AddCqrs()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddCurrentUser()
                .AddDataSeeder()
                .AddIntegrationEventHandlers()
                .AddRepositories()
                .AddGrpc();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseApplicationExceptionHandler();

app.UseEventBus();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.ApplyMigrationAsync<InstallationDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();
