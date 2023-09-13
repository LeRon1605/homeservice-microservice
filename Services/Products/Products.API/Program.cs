using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using FluentValidation;
using Products.Application.Dtos;
using Products.Application.MappingProfiles;
using BuildingBlocks.Presentation.Swagger;
using Products.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ProductService");

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddSwagger("ProductService")
                .AddEventBus(builder.Configuration)
                .AddDbContext(builder.Configuration)
                .AddAutoMapper(typeof(Profiles))
                .AddMediatR()
                .AddValidatorsFromAssembly(typeof(GetProductDto).Assembly)
                .AddRepositories();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();

await builder.Services.RunMigrateAsync();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseApplicationExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();



