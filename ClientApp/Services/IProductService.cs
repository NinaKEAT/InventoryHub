using FullStackApp.Shared.Models;

namespace ClientApp.Services
{
    /// <summary>
    /// Client-side abstraction for fetching products. Components should depend on this instead of HttpClient directly.
    /// </summary>
    public interface IProductService
    {
        Task<Product[]> GetProductsAsync(CancellationToken cancellationToken = default);
    }
}