using System.Text.Json;
using FullStackApp.Shared.Models;

namespace ClientApp.Services
{
    /// <summary>
    /// Simple product service that calls the server API and caches the result client-side.
    /// Provides a single place to add retry, logging, or more complex caching later.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private static Product[]? _cache;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Product[]> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            if (_cache != null)
            {
                return _cache;
            }

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            try
            {
                var response = await _http.GetAsync("/api/productlist", cts.Token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync(cts.Token).ConfigureAwait(false);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var parsed = JsonSerializer.Deserialize<Product[]>(json, options);
                _cache = parsed ?? Array.Empty<Product>();
                return _cache;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("ProductService: request timed out");
                return Array.Empty<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ProductService: error fetching products: {ex.Message}");
                return Array.Empty<Product>();
            }
        }
    }
}