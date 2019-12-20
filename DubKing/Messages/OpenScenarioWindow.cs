using DubKing.Helpers;
using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    public class OpenScenarioWindow
    {
        public Episode Episode { get; set; }
        public Character Character { get; set; }

        public OpenScenarioWindow(Episode episode, Character character = null)
        {
            Episode = episode;
            Character = character;
        }
    }
}
