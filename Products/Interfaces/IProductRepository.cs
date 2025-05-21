using Products.Dto;

namespace Products.Interfaces
{
    public interface IProductRepository<T> : IRepository<T>
    {
        Task<HashSet<int>> GetAllIds();
        Task<ProductDto?> GetProductBySku(string sku);
    }
}
