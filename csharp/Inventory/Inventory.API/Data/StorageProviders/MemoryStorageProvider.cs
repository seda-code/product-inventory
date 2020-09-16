using System.Collections.Generic;
using System.Linq;
using Inventory.API.Models;

namespace Inventory.API.Data.StorageProviders
{
    public class MemoryStorageProvider : IStorageProvider
    {
        IList<Product> products;
        IList<Category> categories;

        public MemoryStorageProvider()
        {
            categories = new[]{
                new Category() { Name = "Hardware" },
                new Category() { Name = "Sotfware" }
            };

            products = new List<Product>{new Product(){
                Name = "Keyboard",
                Units=10,
                Category = categories.Single(x=>x.Name.Equals("Hardware")),
                Value = 20.5f
            }, new Product(){
                Name = "Mouse",
                Units=5,
                Category = categories.Single(x=>x.Name.Equals("Hardware")),
                Value = 40f
            }, new Product(){
                Name = "Game",
                Units=40,
                Category = categories.Single(x=>x.Name.Equals("Sotfware")),
                Value = 30f
            }};
        }

        public void InsertProduct(Product product)
        {
            products.Add(product);
        }

        public void DeleteProduct(string id)
        {
            var product = products.SingleOrDefault(x => x.Id.Equals(id));

            if (products != null)
            {
                products.Remove(product);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product GetProduct(string id)
        {
            return products.SingleOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<Category> GetCategories()
        {
            return categories;
        }

        public Category GetCategory(string id)
        {
            return categories.SingleOrDefault(x => x.Id.Equals(id));
        }

    }
}