using System;

namespace Inventory.API.Models
{
    public class Product : EntityBase<Product>{
        public string Name { get; set; }
        public int Units{get;set;}
        public Category Category{get;set;}
        public float Value{get;set;}

        public override void MapFrom(Product item){
            this.Name = item.Name;
            this.Units = item.Units;
            this.Category = item.Category;
            this.Value = item.Value;
        }
    }
}