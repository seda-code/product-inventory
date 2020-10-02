using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Data.StorageProviders
{
    // TODO: In Progress

    public interface IStorageProvider{
        void InsertProduct(Product product);
        void DeleteProduct(string id);
        Product UpdateProduct(string id, Product product);
        IEnumerable<Product> GetProducts();
        Product GetProduct(string id);
        IEnumerable<Category> GetCategories();
        Category GetCategory(string id);
    }
}