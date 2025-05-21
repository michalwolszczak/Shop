using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class InventoryDeliveryFilter : IFilter<Inventory>
    {
        public IEnumerable<Inventory> Filter(IEnumerable<Inventory> entities)
        {
            return entities.Where(e => e.Shipping == "24h");
        }
    }    
}
