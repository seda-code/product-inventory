using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Data
{
    public interface IDataRepository{
        IEnumerable<Product> GetProducts();        
        Product GetProduct(string id);
    }
}