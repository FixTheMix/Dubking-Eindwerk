using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface ILineRepository<T,U>:IRepositoryExtention<T,U>
    {
        void UpdateAllLines(Character oldCharacter, Character newCharacter, Episode episode, bool markAsRec);
        IList<Line> GetAll(Character character);
        Project GetAll(Project project);
    }
}
