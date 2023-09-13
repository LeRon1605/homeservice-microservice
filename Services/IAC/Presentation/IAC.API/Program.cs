using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "IdentityService");

builder.Services.AddSwagger("IdentityService");
builder.Services.AddServiceCache();
builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();