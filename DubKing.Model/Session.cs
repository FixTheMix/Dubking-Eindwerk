using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class Session
    {

        #region Properties

        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public Session Remarks { get; set; }
        public Task Task { get; set; }
        public ResourcePerson Engineer { get; set; }
        public ResourcePerson Director { get; set; }
        public Project Project { get; set; }
        public List<SessionLog> SessionLogs { get; set; }
        public VoiceTalent VoiceTalent { get; set; }
        
        #endregion
    }
}
