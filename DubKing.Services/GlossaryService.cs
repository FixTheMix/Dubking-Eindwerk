using DubKing.Model;
using DubKing.Repositories.Contracts;
using DubKing.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services
{
    public class GlossaryService : IGlossaryService
    {
        private readonly IGlossaryRepository _glossaryRepository;

        public KeywordComment Create(string keyword, string comment, Project project)
        {
            var key = new KeywordComment() { Keyword = keyword, Comment = comment, Project = project };
            return _glossaryRepository.Create(key);
        }

        public bool Delete(KeywordComment key)
        {
            return _glossaryRepository.Delete(key);
        }

        public KeywordComment[] Get(Project project)
        {
            var result = _glossaryRepository.Get(project);
            for (int i = 0; i < result.Length; i++)
            {
                result[i].Project = project;
            }
            return result;
        }

        public bool Update(KeywordComment key)
        {
            return _glossaryRepository.Update(key);
        }

        public GlossaryService(IGlossaryRepository glossaryRepository)
        {
            _glossaryRepository = glossaryRepository;
        }
    }
}
