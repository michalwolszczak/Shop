using Products.Entities;
using Products.Interfaces;

namespace Products.Filters
{
    public class PriceConnectedToInventoryFilterFactory : IFilterFactory<Price>
    {
        private readonly IInventoryRepository<Inventory> _inventoryRepository;

        public PriceConnectedToInventoryFilterFactory(IInventoryRepository<Inventory> inventoryRepositor)
        {
            _inventoryRepository = inventoryRepositor;
        }

        public async Task<IFilter<Price>> CreateAsync()
        {
            var existingInventoryIds = await _inventoryRepository.GetAllSKUs();

            return new PriceConnectedToInventoryFilter(existingInventoryIds);
        }
    }
}
