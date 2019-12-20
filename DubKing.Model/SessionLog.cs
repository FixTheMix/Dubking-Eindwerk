using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class SessionLog
    {

        #region Properties

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public User User { get; set; }
        public int LineCount { get; set; }
        public Project Project { get; set; }
        public Session Session { get; set; }
        public VoiceTalent VoiceTalent { get; set; }

        #endregion

    }
}
