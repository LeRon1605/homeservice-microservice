using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Swagger;
using IAC.API.Extensions;
using IAC.Application.Grpc.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "IdentityService");

builder.Services.AddSwagger("IdentityService")
                .AddRepositories()
                .AddMapper()
                .AddServices()
                .AddApplicationExceptionHandler()
                .AddEventBus(builder.Configuration)
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddIdentity(builder.Configuration, builder.Environment)
                .AddApplicationAuthentication(builder.Configuration)
                .AddConfiguration(builder.Configuration)
                .AddGrpc();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddControllers();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseApplicationExceptionHandler();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<AuthGrpcService>();
app.MapControllers();

await app.ApplyMigrationAsync(app.Logger);
await app.SeedDataAsync();

app.Run();