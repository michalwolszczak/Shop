using CsvHelper.Configuration;
using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class PriceConnectedToInventoryFilter : IFilter<Price>
    {
        private readonly HashSet<string> _existingInventoryIds;

        public PriceConnectedToInventoryFilter(HashSet<string> existingInventoryIds)
        {
            _existingInventoryIds = existingInventoryIds;
        }

        public IEnumerable<Price> Filter(IEnumerable<Price> entities)
        {
            return entities.Where(e => !string.IsNullOrWhiteSpace(e.Sku) && _existingInventoryIds.Contains(e.Sku));
        }
    }
}
