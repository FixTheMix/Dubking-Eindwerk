using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface IGlossaryRepository
    {
        KeywordComment Create(KeywordComment key);
        bool Update(KeywordComment key);
        bool Delete(KeywordComment key);
        KeywordComment[] Get(Project project);
    }
}
