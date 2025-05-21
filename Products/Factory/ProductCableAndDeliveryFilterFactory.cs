using Products.Entities;
using Products.Filters;
using Products.Interfaces;

namespace Products.Factory
{
    public class ProductCableAndDeliveryFilterFactory : IFilterFactory<Product>
    {
        public Task<IFilter<Product>> CreateAsync()
        {
            return Task.FromResult<IFilter<Product>>(new ProductCableAndDeliveryFilter());
        }
    }
}