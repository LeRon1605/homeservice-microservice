using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Services.Common.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwagger("HomeAppService");
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerForOcelotUI().UseOcelot();

app.UseRouting();

app.Run();