using System;

namespace Inventory.API.Models
{
    public class Product{
        public string Id { get; set; }
        public string Name { get; set; }
        public int Units{get;set;}
        public Category Category{get;set;}
        public float Value{get;set;}
        
    }
}