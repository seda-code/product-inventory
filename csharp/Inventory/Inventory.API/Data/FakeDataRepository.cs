using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.API.Models;

namespace Inventory.API.Data
{
    public class FakeDataRepository : IDataRepository
    {
        private IList<Product> products;
        readonly IEnumerable<Category> categories;

        public FakeDataRepository()
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
        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product GetProduct(string id)
        {
            return products.FirstOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<Category> GetCategories()
        {
            return categories;
        }

        public Category GetCategory(string id)
        {
            return categories.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void SaveProduct(Product product)
        {
            products.Add(product);
        }

        public void DeleteProduct(string id)
        {
            var product = products.FirstOrDefault(x => x.Id.Equals(id));
            products.Remove(product);
        }
    }
}