using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface IRemoveUnused:IRepositoryExtention<Character, Project>
    {
        Character Create(Character item, bool removeUnused = true);
        void RemoveUnused();
    }
}
