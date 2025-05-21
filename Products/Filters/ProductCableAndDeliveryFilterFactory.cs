
using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class ProductCableAndDeliveryFilterFactory : IFilterFactory<Product>
    {
        public Task<IFilter<Product>> CreateAsync()
        {
            return Task.FromResult<IFilter<Product>>(new ProductCableAndDeliveryFilter());
        }
    }
}