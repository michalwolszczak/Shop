﻿using Products.Entities;
using Products.Filters;
using Products.Interfaces;

namespace Products.Factory
{
    public class InventoryConnectedToProductAndDeliveryFilterFactory : IFilterFactory<Inventory>
    {
        private readonly IProductRepository<Product> _repository;

        public InventoryConnectedToProductAndDeliveryFilterFactory(IProductRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IFilter<Inventory>> CreateAsync()
        {
            var productIds = await _repository.GetAllIds();

            return new InventoryConnectedToProductAndDeliveryFilter(productIds);
        }
    }
}
