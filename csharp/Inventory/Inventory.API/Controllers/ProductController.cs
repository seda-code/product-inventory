using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Inventory.API.Models;
using System.Threading.Tasks;
using Inventory.API.Data.Repositories;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IDataRepository<Product> dataRepository;

        public ProductController(IDataRepository<Product> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("{id}")]
        // https://localhost:5001/api/product/1
        public Product Get(string id)
        {
            return dataRepository.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        // https://localhost:5001/api/product/GetProducts
        public IEnumerable<Product> GetProducts()
        {
            return dataRepository.Get();
        }

        [HttpPost]
        public ActionResult<Product> AddProduct([FromBody] Product product)
        {
            var savedProduct = dataRepository.Insert(product);

            return Ok(savedProduct);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Product> UpdateProduct(string id, [FromBody] Product product){
            var updatedProduct = dataRepository.Update(id, product);

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(string id){
            dataRepository.Delete(id);

            return Ok($"Product {id} deleted");
        }
    }
}