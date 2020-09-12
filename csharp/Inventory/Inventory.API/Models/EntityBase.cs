using System;

namespace Inventory.API.Models
{
    public class EntityBase{
        public string Id{get;set;}

        public EntityBase() : this(Guid.NewGuid().ToString()){}
        
        public EntityBase(string id)
        {
            Id = id;
        }
    }
}