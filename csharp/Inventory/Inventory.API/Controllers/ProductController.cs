using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Inventory.API.Models;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        // https://localhost:5001/api/product/1
        public Product Get(string id)
        {
            // TODO: Get product by ID.

            var product = new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Dummy product"
            };

            // JsonSerializer.Serialize(product)

            return product;
        }

        [HttpGet]
        [Route("[action]")]
        // https://localhost:5001/api/product/GetProducts
        public IEnumerable<Product> GetProducts()
        {
            // TODO: Get a list of products

            var products = new[]{new Product(){
                Id = Guid.NewGuid().ToString(),
                Name = "Keyboard"
            }, new Product(){
                Id = Guid.NewGuid().ToString(),
                Name = "Mouse"
            }};
            
            return products;
            // return JsonSerializer.Serialize(products);
        }
    }
}