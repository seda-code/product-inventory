using System.Collections.Generic;
using Inventory.API.Models;

namespace Inventory.API.Data.Repositories
{
    public interface IDataRepository<T> where T : EntityBase<T>
    {
        IEnumerable<T> Get();
        T Get(string id);
        T Insert(T item);
        T Update(string id, T item);
        void Delete(string id);
    }
}