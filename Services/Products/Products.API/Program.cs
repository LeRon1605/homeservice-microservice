using BuildingBlocks.Application.Behaviors;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Products.API.Middlewares;
using Products.Application.Dtos;
using Products.Application.MappingProfiles;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.Data;
using Products.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(Profiles));

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IReadOnlyRepository<Product>, ProductReadonlyRepository>();
builder.Services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<ProductDbContext>>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetProductDto).Assembly);

    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(GetProductDto).Assembly);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
await context.Database.MigrateAsync();
await new ProductDbContextSeed().SeedAsync(context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler(app.Environment);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();