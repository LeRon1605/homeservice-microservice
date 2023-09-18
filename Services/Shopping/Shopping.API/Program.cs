using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authentication;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.Swagger;
using Serilog;
using Shopping.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "ShoppingService");

builder.Services.AddSwagger("ShoppingService")
				.AddEventBus(builder.Configuration)
				.AddDatabase(builder.Configuration, builder.Environment)
				.AddRepositories()
				.AddMapper()
				.AddCqrs()
				.AddHomeServiceAuthentication(builder.Configuration)
				.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.MapControllers();

await app.ApplyMigrationAsync(app.Logger);

app.Run();