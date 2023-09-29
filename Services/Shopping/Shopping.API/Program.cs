using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Newtonsoft.Json.Converters;
using Serilog;
using Shopping.API.Extensions;
using Shopping.Application.Grpc.Services;
using Shopping.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ShoppingService");

builder.Services.AddSwagger("ShoppingService")
                .AddEfCoreDbContext<OrderDbContext>(builder.Configuration)
                .AddEventBus(builder.Configuration)
                .AddRepositories()
                .AddApplicationExceptionHandler()
                .AddMapper()
                .AddCqrs()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddCurrentUser()
                .AddDataSeeder()
                .AddIntegrationEventHandlers()
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

app.MapGrpcService<ShoppingProductGrpcService>();
app.MapControllers();

await app.ApplyMigrationAsync<OrderDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();