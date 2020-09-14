using System;
using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Services
{
    public interface IInventoryService{
        IEnumerable<Product> GetProducts();
        Product GetProduct(string id);
        IEnumerable<Category> GetCategories();
        Category GetCategory(string id);

        void SaveProduct(Product product);
        void DeleteProduct(string id);
    }
}