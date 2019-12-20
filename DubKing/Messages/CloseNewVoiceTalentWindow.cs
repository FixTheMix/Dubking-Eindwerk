using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Messages
{
    class CloseNewVoiceTalentWindow
    {
        public bool IsSaved { get; set; }
        public VoiceTalent NewVoiceTalent { get; set; }

        public CloseNewVoiceTalentWindow(bool isSaved, VoiceTalent newVoiceTalent)
        {
            IsSaved = isSaved;
            NewVoiceTalent = newVoiceTalent;
        }
    }
}
