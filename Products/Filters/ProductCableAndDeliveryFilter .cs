using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class ProductCableAndDeliveryFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> entities)
        {
            return entities
                .Where(p => p.IsWire == 0 && p.Shipping == "24h");
        }
    }
}
