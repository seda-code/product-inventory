using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Inventory.API.Models;
using System.Threading.Tasks;
using Inventory.API.Services;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IInventoryService inventoryService;

        public ProductController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        [HttpGet]
        [Route("{id}")]
        // https://localhost:5001/api/product/1
        public Product Get(string id)
        {
            return inventoryService.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        // https://localhost:5001/api/product/GetProducts
        public IEnumerable<Product> GetProducts()
        {
            return inventoryService.Get();
        }

        [HttpPost]
        public ActionResult<Product> AddProduct([FromBody] string product)
        {
            var value = JsonSerializer.Deserialize<Product>(product);
            //System.Diagnostics.Debug.Write(product.Name);

            return CreatedAtAction(nameof(AddProduct), new Product() { Name = "aa" });
        }
    }
}