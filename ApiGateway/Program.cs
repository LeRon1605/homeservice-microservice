using ApiGateway.Extensions;
using ApiGateway.Middlewares;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Extension;
using Newtonsoft.Json.Converters;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "HomeService");

builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddGrpcClientServices(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddOcelot();
builder.Services.AddApplicationCors();
builder.Services.AddApiGatewaySwagger(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClients(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

app.UseCors("HomeService");

app.UseSwagger();
app.UseSwaggerForOcelotUI();

app.UseCustomExceptionHandler(app.Environment);

app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014

app.UseOcelot();

app.Run();