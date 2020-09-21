using System;

namespace Inventory.API.Models
{
    public class Category : EntityBase<Category>{
        public string Name { get; set; }

        public override void MapFrom(Category item)
        {
            this.Name = item.Name;
        }
    }
}