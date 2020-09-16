using System.Collections.Generic;
using Inventory.API.Data.StorageProviders;
using Inventory.API.Models;

namespace Inventory.API.Data.Repositories
{
    public class CategoryRepository : IDataRepository<Category>
    {
        private readonly IStorageProvider storageProvider;

        public CategoryRepository(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public void Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Category> Get()
        {
            return storageProvider.GetCategories();
        }

        public Category Get(string id)
        {
            return storageProvider.GetCategory(id);
        }

        public Category Insert(Category item)
        {
            throw new System.NotImplementedException();
        }
    }
}