using System;
using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Services
{
    public interface IInventoryService{
        IEnumerable<Product> Get();
        Product Get(string id);
    }
}