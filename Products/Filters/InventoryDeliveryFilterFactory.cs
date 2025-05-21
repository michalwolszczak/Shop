using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class InventoryDeliveryFilterFactory : IFilterFactory<Inventory>
    {
        public Task<IFilter<Inventory>> CreateAsync()
        {
            return Task.FromResult<IFilter<Inventory>>(new InventoryDeliveryFilter());
        }
    }
}
