using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    class SendCharacterToEdit
    {
        public Character Character { get; set; }
        public SendCharacterToEdit(Character character)
        {
            Character = character;
        }
    }
}
