using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class InventoryConnectedToProductAndDeliveryFilter : IFilter<Inventory>
    {
        private readonly HashSet<int> _existingProductIds;

        public InventoryConnectedToProductAndDeliveryFilter(HashSet<int> existingProductIds)
        {
            _existingProductIds = existingProductIds;
        }

        public IEnumerable<Inventory> Filter(IEnumerable<Inventory> entities)
        {
            return entities.Where(x => 
            !string.IsNullOrWhiteSpace(x.Sku) 
            && x.Shipping == "24h"
            && _existingProductIds.Contains(x.ProductId));
        }
    }
}