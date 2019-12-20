using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueService
{
    public class DialogueBoxFactory : IShowDialogue
    {
        public void ShowErrorDialogue(string msg)
        {
            var dial = new DialogueBoxError(msg);
            dial.ShowDialog();
        }
    }
}
