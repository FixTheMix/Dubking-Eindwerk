using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IRepositoryAsync<T, U>
    {
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetAllAsync(T item);
        Task<IList<T>> GetAllAsync(U item);
        Task<T> SaveAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
