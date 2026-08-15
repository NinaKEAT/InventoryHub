using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Use the API server URL in development so the client can reach /api/productlist.
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5100";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

// Register client-side services (abstraction over HttpClient)
builder.Services.AddScoped<ClientApp.Services.IProductService, ClientApp.Services.ProductService>();

await builder.Build().RunAsync();
