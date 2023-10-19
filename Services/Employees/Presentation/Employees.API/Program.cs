using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using BuildingBlocks.Presentation.MailSender;
using BuildingBlocks.Presentation.Swagger;
using Employees.API.Extensions;
using Employees.Application.Mapper;
using Employees.Infrastructure;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "EmployeeService");

builder.Services.AddSwagger("EmployeeService")
    .AddEfCoreDbContext<EmployeeDbContext>(builder.Configuration)
    .AddEventBus(builder.Configuration)
    .AddRepositories()
    .AddAutoMapper(typeof(EmployeeMapper))
    .AddIntegrationEventHandlers()
    .AddApplicationExceptionHandler()
    .AddCqrs()
    .AddHomeServiceAuthentication(builder.Configuration)
    .AddCurrentUser()
    .AddDataSeeder()
    .AddEmailSetting(builder.Configuration)
    .AddGrpc();
// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseEventBus();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseApplicationExceptionHandler();
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.ApplyMigrationAsync<EmployeeDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();