using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class ConfirmGlossaryKeywordDeleteMessage
    {
        public KeywordComment Keyword { get; set; }
        public bool CanDelete { get; set; } = false;
        public ConfirmGlossaryKeywordDeleteMessage(KeywordComment keyword)
        {
            Keyword = keyword;
        }
    }
}
