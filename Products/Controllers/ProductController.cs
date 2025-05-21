using Microsoft.AspNetCore.Mvc;
using Products.Dto;
using Products.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, ILogger<ShopController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pobiera informacje o produkcie z bazy")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetProductResponse), ContentTypes = new string[] { "application/json" })]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails), ContentTypes = new string[] { "application/problem+json" })]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails), ContentTypes = new string[] { "application/problem+json" })]
        public async Task<ActionResult<GetProductResponse>> GetProductBySku([Required][FromQuery] string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                return BadRequest("SKU cannot be null or empty.");
            }

            var product = await _productService.GetProductBySku(sku);
            if (product == null)
            {
                return NotFound($"Product with SKU {sku} not found.");
            }
            return Ok(new GetProductResponse(product));
        }
    }
}
