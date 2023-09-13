using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Customers.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "CustomerService");

builder.Services.AddSwagger("CustomerService");
builder.Services.AddDatabase(builder.Configuration, builder.Environment)
    .AddRepositories()
    .AddCqrs()
    .AddMapper()
    .AddService()
    .AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();