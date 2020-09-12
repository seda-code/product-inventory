using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Models;
using Inventory.API.Services;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // https://localhost:5001/api/category
    public class CategoryController
    {
        private readonly IInventoryService inventoryService;

        public CategoryController(IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
        }

        [HttpGet]
        [Route("{id}")]
        // https://localhost:5001/api/category/1
        public Category Get(string id)
        {
            return inventoryService.GetCategory(id);
        }

        [HttpGet]
        [Route("[action]")]
        // https://localhost:5001/api/category/GetCategories
        public IEnumerable<Category> GetCategories()
        {
            return inventoryService.GetCategories();
        }

    }
}