using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Models;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // https://localhost:5001/api/category
    public class CategoryController
    {
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return new[]{new Category(){
                Id = Guid.NewGuid().ToString(),
                Name = "Hardware"
            }, new Category(){
                Id = Guid.NewGuid().ToString(),
                Name = "Software"
            }};
        }
    }
}