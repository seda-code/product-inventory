using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Data
{
    public interface IDataRepository{
        IEnumerable<Product> GetProducts();        
        Product GetProduct(string id);

        IEnumerable<Category> GetCategories();
        Category GetCategory(string id);

        Product SaveProduct(Product product);
        void DeleteProduct(string id);
    }
}