using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServerApp.Middleware;

// Minimal API Back-End (ServerApp)
// - Exposes /api/productlist with nested Category objects
// - Adds CORS to allow browser requests from the client
// - Uses in-memory caching to avoid recomputing the list on each request
// - Configures JSON options to use camelCase (id,name,price,stock,category)

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMemoryCache();
// Configure JSON naming policy (camelCase) so API returns id/name/price/stock/category
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

builder.Services.AddCors();

// Register controllers and middleware
builder.Services.AddControllers();
// Wire up the product service implementation
builder.Services.AddSingleton<ServerApp.Services.IProductService, ServerApp.Services.ProductService>();

var app = builder.Build();

// Use a small request-logging middleware for development diagnostics
app.UseRequestLogging();

// CORS: allow client access. In production, restrict origins appropriately.
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Map attribute routed controllers (ProductsController)
app.MapControllers();

// Keep the app running
app.Run();