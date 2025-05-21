using Products.Dto;
using Products.Entities;
using Products.Interfaces;

namespace Products.Services
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductBySku(string sku);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository<Product> _productRepository;

        public ProductService(IProductRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> GetProductBySku(string sku)
        {
            var result = await _productRepository.GetProductBySku(sku);

            return result;
        }
    }
}
