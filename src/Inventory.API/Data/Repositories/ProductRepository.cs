using System.Collections.Generic;
using Inventory.API.Data.StorageProviders;
using Inventory.API.Models;

namespace Inventory.API.Data.Repositories
{
    public class ProductRepository : IDataRepository<Product>
    {
        private readonly IStorageProvider storageProvider;

        public ProductRepository(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }
        public void Delete(string id)
        {
            storageProvider.DeleteProduct(id);
        }

        public IEnumerable<Product> Get()
        {
            return storageProvider.GetProducts();
        }

        public Product Get(string id)
        {
            return storageProvider.GetProduct(id);
        }

        public Product Insert(Product item)
        {
            try
            {
                storageProvider.InsertProduct(item);
            }
            catch
            {
                throw new System.Exception();
            }

            return item;
        }

        public Product Update(string id, Product item)
        {
            try
            {
                return storageProvider.UpdateProduct(id, item);
            }
            catch
            {
                throw new System.Exception();
            }
        }
    }
}