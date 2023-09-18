using BuildingBlocks.Application.Identity;
using BuildingBlocks.EventBus.Interfaces;
using BuildingBlocks.Infrastructure.Serilog;
using BuildingBlocks.Presentation.Authorization;
using BuildingBlocks.Presentation.DataSeeder;
using BuildingBlocks.Presentation.EventBus;
using BuildingBlocks.Presentation.ExceptionHandlers;
using BuildingBlocks.Presentation.Swagger;
using IAC.API.Extensions;
using IAC.API.Grpc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "IdentityService");

builder.Services.AddSwagger("IdentityService")
                .AddApplicationAuthentication(builder.Configuration)
                .AddRepositories()
                .AddMapper()
                .AddServices()
                .AddApplicationExceptionHandler()
                .AddEventBus(builder.Configuration)
                .AddDatabase(builder.Configuration, builder.Environment)
                .AddIdentity(builder.Configuration, builder.Environment)
                .AddConfiguration(builder.Configuration)
                .AddGrpcReflection()
                .AddGrpc();

builder.Services.AddControllers();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.UseApplicationExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGrpcService<AuthService>();
app.MapControllers();

await app.ApplyMigrationAsync(app.Logger);
await app.SeedDataAsync();

app.Run();