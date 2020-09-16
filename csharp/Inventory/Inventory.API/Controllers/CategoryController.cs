using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Models;
using Inventory.API.Data.Repositories;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // https://localhost:5001/api/category
    public class CategoryController
    {
        private readonly IDataRepository<Category> dataRepository;

        public CategoryController(IDataRepository<Category> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("{id}")]
        // https://localhost:5001/api/category/1
        public Category Get(string id)
        {
            return dataRepository.Get(id);
        }

        [HttpGet]
        [Route("[action]")]
        // https://localhost:5001/api/category/GetCategories
        public IEnumerable<Category> GetCategories()
        {
            return dataRepository.Get();
        }

    }
}