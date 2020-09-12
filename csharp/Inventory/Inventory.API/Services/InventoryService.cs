using System;
using System.Collections.Generic;
using Inventory.API.Models;
using Inventory.API.Data;

namespace Inventory.API.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IDataRepository dataRepository;

        public InventoryService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public IEnumerable<Product> Get()
        {
            return dataRepository.GetProducts();
        }

        public Product Get(string id)
        {
            return dataRepository.GetProduct(id);
        }
    }
}