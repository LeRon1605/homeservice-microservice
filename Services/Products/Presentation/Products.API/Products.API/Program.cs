using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.Swagger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ShoppingService");

builder.Services.AddSwagger("ProductService")
                .AddEventBus(builder.Configuration);

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();