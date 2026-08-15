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
        private static readonly SemaphoreSlim _cacheLock = new(1, 1);
        private static Task<Product[]>? _inFlightRequest;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Product[]> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            if (_cache is not null)
            {
                return _cache;
            }

            if (_inFlightRequest is not null)
            {
                return await _inFlightRequest.WaitAsync(cancellationToken).ConfigureAwait(false);
            }

            await _cacheLock.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                if (_cache is not null)
                {
                    return _cache;
                }

                _inFlightRequest = FetchProductsAsync(cancellationToken);
                var result = await _inFlightRequest.ConfigureAwait(false);
                _cache = result;
                return result;
            }
            finally
            {
                _inFlightRequest = null;
                _cacheLock.Release();
            }
        }

        private async Task<Product[]> FetchProductsAsync(CancellationToken cancellationToken)
        {
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
                return parsed ?? Array.Empty<Product>();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("ProductService: request timed out");
                return Array.Empty<Product>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"ProductService: malformed JSON response: {ex.Message}");
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