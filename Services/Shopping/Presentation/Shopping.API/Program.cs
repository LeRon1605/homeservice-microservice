using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Swagger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ShoppingService");

builder.Services.AddSwagger("ShoppingService");
builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();