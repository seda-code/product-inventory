using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Data.Repositories
{
    // public interface IDataRepository{
    //     IEnumerable<Product> GetProducts();        
    //     Product GetProduct(string id);

    //     IEnumerable<Category> GetCategories();
    //     Category GetCategory(string id);

    //     Product SaveProduct(Product product);
    //     void DeleteProduct(string id);
    // }

    public interface IDataRepository<T> where T : EntityBase
    {
        IEnumerable<T> Get();
        T Get(string id);
        T Insert(T item);
        void Delete(string id);
    }
}