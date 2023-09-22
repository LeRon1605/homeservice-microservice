using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.Cloudinary;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EfCore;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Extension;
using FluentValidation;
using Products.Application.Dtos;
using Products.Application.MappingProfiles;
using BuildingBlocks.Presentation.Swagger;
using Products.API.Extensions;
using Products.Application.Grpc.Services;
using Products.Infrastructure.EfCore.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ProductService");

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddSwagger("ProductService")
                .AddCloudinary(builder.Configuration)
                .AddEfCoreDbContext<ProductDbContext>(builder.Configuration)
                .AddApplicationExceptionHandler()
                .AddEventBus(builder.Configuration)
                .AddAutoMapper(typeof(Profiles))
                .AddMediatR()
                .AddHomeServiceAuthentication(builder.Configuration)
                .AddDataSeeder()
                .AddValidatorsFromAssembly(typeof(GetProductDto).Assembly)
                .AddServices()
                .AddDomainServices()
                .AddRepositories()
                .AddCurrentUser()
                .AddGrpc();;

var app = builder.Build();

// var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseApplicationExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<ProductGrpcService>();
app.MapControllers();

await app.ApplyMigrationAsync<ProductDbContext>(app.Logger);
await app.SeedDataAsync();

app.Run();



