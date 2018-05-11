using System.Collections.Generic;

namespace BloodTypes.Core.Interfaces
{
    public interface IRepository<T>
    {
        T Get(string id);
        IEnumerable<T> GetAll();
        bool Add(T item);
        bool AddMany(IEnumerable<T> items);
        bool Update(T item);
        bool Remove(T item);
    }
}
