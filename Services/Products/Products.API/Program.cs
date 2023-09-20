using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.Cloudinary;
using BuildingBlocks.Presentation.DataSeeder;
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
    .AddCloudinary(builder.Configuration)
    .AddApplicationExceptionHandler()
    .AddEventBus(builder.Configuration)
    .AddDbContext(builder.Configuration)
    .AddAutoMapper(typeof(Profiles))
    .AddMediatR()
    .AddHomeServiceAuthentication(builder.Configuration)
    .AddDataSeeder()
    .AddValidatorsFromAssembly(typeof(GetProductDto).Assembly)
    .AddServices()
    .AddDomainServices()
    .AddRepositories();
                
                

builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseApplicationExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.ApplyMigrationAsync(app.Logger);
await app.SeedDataAsync();

app.Run();



