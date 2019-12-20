using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Contracts
{
    public interface ILanguageRepository
    {
        int CreateLanguage(string language);
        List<string> GetLanguages();
    }
}
