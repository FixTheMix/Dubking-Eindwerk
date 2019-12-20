using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface IRepository<T>
    {
        T Create(T item);
        List<T> GetAll();
        T GetById(int id);
        void Update(T updatedItem);
        void Delete(T deletedItem);
    }
}
