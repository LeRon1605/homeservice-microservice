using BuildingBlocks.Presentation.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwagger("ProductService");
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();