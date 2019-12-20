using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class LoadScenarioMessage
    {
        public Episode Episode { get; set; }
        public Character Character { get; set; }
        public LoadScenarioMessage(Episode ep, Character character = null)
        {
            Episode = ep;
            if (character != null)
            {
                Character = character;
            }
        }
    }
}
