using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
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
                .AddCqrs()
                .AddMapper()
                .AddService()
                .AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.ApplyMigrationAsync(app.Logger);

app.Run();