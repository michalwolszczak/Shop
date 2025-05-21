using Microsoft.AspNetCore.Mvc;
using Products.Entities;
using Products.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/shop")]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IImportService<Product> _productImportService;
        private readonly IImportService<Inventory> _inventoryImportService;
        private readonly IImportService<Price> _priceImportService;

        public ShopController(ILogger<ShopController> logger, IImportService<Product> productImportService, IImportService<Inventory> inventoryImportService, IImportService<Price> priceImportService)
        {
            _logger = logger;
            _productImportService = productImportService;
            _inventoryImportService = inventoryImportService;
            _priceImportService = priceImportService;
        }

        [HttpPost("import")]
        [SwaggerOperation(Summary = "Pobiera dane a nastêpnie zapisuje lokalnie oraz do bazy")]
        [SwaggerResponse(StatusCodes.Status204NoContent, ContentTypes = new string[] { "application/json" })]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails), ContentTypes = new string[] { "application/problem+json" })]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails), ContentTypes = new string[] { "application/problem+json" })]
        public async Task<IActionResult> Import()
        {
            await _productImportService.ImportAsync("https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv", "Products.csv");
            await _inventoryImportService.ImportAsync("https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv", "Inventory.csv");
            await _priceImportService.ImportAsync("https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv", "Prices.csv");

            return NoContent();
        }
    }
}