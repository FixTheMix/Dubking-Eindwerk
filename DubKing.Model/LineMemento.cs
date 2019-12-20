using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class LineMemento
    {
        public Character Character { get; private set; }
        public string Text { get; private set; }
        public Timecode Timecode { get; private set; }
        public string Comment { get; private set; }

        public LineMemento(Character character, string text, Timecode tc, string comment)
        {
            Character = character;
            Text = text;
            Timecode = tc;
            Comment = comment;

        }
    }
}
