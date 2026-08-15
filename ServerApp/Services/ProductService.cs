using Microsoft.Extensions.Caching.Memory;
using FullStackApp.Shared.Models;

namespace ServerApp.Services
{
    /// <summary>
    /// Simple in-memory product provider. In production this would fetch data from a database.
    /// Uses IMemoryCache to minimize repeated work and serialization.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "productList_v1";

        public ProductService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<Product[]> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(CacheKey, out Product[]? cached))
            {
                // cached may be null if something removed it; ensure non-nullable return
                return Task.FromResult(cached ?? Array.Empty<Product>());
            }

            // Construct a static list to mimic a data store
            var products = new Product[]
            {
                new Product { Id = 1, Name = "Laptop", Price = 1200.50, Stock = 25, Category = new Category { Id = 101, Name = "Electronics" } },
                new Product { Id = 2, Name = "Headphones", Price = 50.00, Stock = 100, Category = new Category { Id = 102, Name = "Accessories" } }
            };

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(1)
            };

            _cache.Set(CacheKey, products, cacheOptions);

            return Task.FromResult(products);
        }
    }
}