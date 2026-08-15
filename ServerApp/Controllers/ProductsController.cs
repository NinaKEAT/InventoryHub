using Microsoft.AspNetCore.Mvc;
using ServerApp.Services;
using FullStackApp.Shared.Models;

namespace ServerApp.Controllers
{
    /// <summary>
    /// API controller exposing /api/productlist. Keeps routing explicit and supports future expansion.
    /// </summary>
    [ApiController]
    [Route("api/productlist")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsAsync(cancellationToken).ConfigureAwait(false);
            return Ok(products);
        }
    }
}