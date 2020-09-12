using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.API.Models;

namespace Inventory.API.Data
{
    public class FakeDataRepository : IDataRepository
    {
        readonly IEnumerable<Product> products;
        readonly IEnumerable<Category> categories;

        public FakeDataRepository()
        {
            categories = new[]{
                new Category() { Name = "Hardware" },
                new Category() { Name = "Sotfware" }
            };

            products = new[]{new Product(){
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
    }
}