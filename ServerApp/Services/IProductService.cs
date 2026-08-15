using FullStackApp.Shared.Models;

namespace ServerApp.Services
{
    /// <summary>
    /// Abstraction for product data retrieval. Keeps controller thin and testable.
    /// </summary>
    public interface IProductService
    {
        Task<Product[]> GetProductsAsync(CancellationToken cancellationToken = default);
    }
}