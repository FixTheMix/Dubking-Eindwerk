using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    class SelectActor
    {
        public Character SelectedCharacter { get; set; }
        public SelectActor(Character character)
        {
            SelectedCharacter = character;
        }
    }
}
