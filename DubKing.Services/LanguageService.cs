using DubKing.Repositories.Contracts;
using DubKing.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services
{
    class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        public List<string> GetLanguages()
        {
            return _languageRepository.GetLanguages();
        }
        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }
    }
}
