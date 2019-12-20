using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface IRepositoryExtention<T,U>:IRepository<T>
    {
        IList<T> GetAll(U item);
    }
}
