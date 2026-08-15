using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerApp.Middleware;
using ServerApp.Services;

// Inventory API and supporting services.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Product compatibility service
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

app.UseRequestLogging();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.MapOpenApi();
app.MapControllers();

app.Run();