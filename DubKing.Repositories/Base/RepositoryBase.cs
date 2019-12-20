using DialogueService;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories.Base
{
    public abstract class RepositoryBase
    {
        private readonly IShowDialogue _dialogueService;
        protected readonly string _connectionString;

        public RepositoryBase(IShowDialogue dialogueService, IConnectionStringReader connectionStringReader)
        {
            _dialogueService = dialogueService;
            _connectionString = connectionStringReader.GetConnectionString();
        }

        protected void ShowErrorDialogue(string msg)
        {
            _dialogueService.ShowErrorDialogue(msg);
        }
    }
}
