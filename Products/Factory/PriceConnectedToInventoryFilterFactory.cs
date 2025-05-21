using Products.Entities;
using Products.Filters;
using Products.Interfaces;

namespace Products.Factory
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
