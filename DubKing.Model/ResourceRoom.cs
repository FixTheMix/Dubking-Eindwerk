using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public enum RoomFunction { Audio, Video }
    public enum RoomEquipped { Mix51, Mix20, Edit51, Edit20, Recording }
    public class ResourceRoom
    {
       


        #region Properties

        public string Name { get; set; }
        public RoomFunction Function { get; set; }

        public RoomEquipped Equipped { get; set; }
        public List<Session> Sessions { get; set; }


        #endregion

    }
}
