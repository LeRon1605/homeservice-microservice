using Services.Common.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger("ShoppingService");
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();