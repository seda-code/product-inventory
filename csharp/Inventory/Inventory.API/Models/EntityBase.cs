using System;

namespace Inventory.API.Models
{
    public abstract class EntityBase<T>{
        public string Id{get;set;}

        public EntityBase() : this(Guid.NewGuid().ToString()){}
        
        public EntityBase(string id)
        {
            Id = id;
        }

        public abstract void MapFrom(T item);
    }
}