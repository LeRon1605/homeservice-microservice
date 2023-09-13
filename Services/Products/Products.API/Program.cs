using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authorization;
using FluentValidation;
using Products.API.Middlewares;
using Products.Application.Dtos;
using Products.Application.MappingProfiles;
using BuildingBlocks.Presentation.Swagger;
using Products.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ProductService");

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwagger("ProductService");

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Profiles));

builder.Services.AddMediatR();

builder.Services.AddValidatorsFromAssembly(typeof(GetProductDto).Assembly);

builder.Services.AddRepositories();

// Access current user info
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();


await builder.Services.RunMigrateAsync();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler(app.Environment);

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



