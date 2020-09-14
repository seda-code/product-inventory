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
        public IEnumerable<Product> GetProducts()
        {
            return dataRepository.GetProducts();
        }

        public Product GetProduct(string id)
        {
            return dataRepository.GetProduct(id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return dataRepository.GetCategories();
        }

        public Category GetCategory(string id)
        {
            return dataRepository.GetCategory(id);
        }

        public void SaveProduct(Product product){
            dataRepository.SaveProduct(product);
        }
        
        public void DeleteProduct(string id){
            dataRepository.DeleteProduct(id);
        }
    }
}