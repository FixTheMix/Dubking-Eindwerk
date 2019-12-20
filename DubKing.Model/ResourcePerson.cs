using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public enum PersonFunction { Sound, Image, Directing}
    public enum PersonCompetence { Mix51, Mix20,Edit51, Edit20, Recording}
    public class ResourcePerson
    {

        #region Properties

        public string Name { get; set; }
        public PersonFunction Function { get; set; }
        public PersonCompetence Competence { get; set; }
        public List<Session> Session { get; set; }

        #endregion

    }
}
