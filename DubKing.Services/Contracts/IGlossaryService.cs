using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IGlossaryService
    {
        KeywordComment Create(string keyword, string comment, Project project);
        bool Update(KeywordComment key);
        bool Delete(KeywordComment key);
        KeywordComment[] Get(Project project);
    }
}
