using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.Swagger;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "HomeAppService");

builder.Services.AddControllers();

builder.Services.AddSwagger("HomeAppService");
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddApplicationCors();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseCors("HomeService");
app.UseSwagger();
app.UseSwaggerForOcelotUI().UseOcelot();

app.UseRouting();

app.Run();