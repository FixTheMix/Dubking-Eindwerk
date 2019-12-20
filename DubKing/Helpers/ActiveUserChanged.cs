using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    public class ActiveUserChanged
    {
        public User ActiveUser { get; set; }
        public ActiveUserChanged(User activeUser)
        {
            ActiveUser = activeUser;
        }

    }
}
