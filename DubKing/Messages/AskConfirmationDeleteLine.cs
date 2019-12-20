using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class AskConfirmationDeleteLine
    {
        public bool IsConfirmed { get; set; }
        public AskConfirmationDeleteLine()
        {
            IsConfirmed = false;
        }
    }
}
