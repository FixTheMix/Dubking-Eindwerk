using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Helpers
{
    class MessageCloseNewUserWindow
    {
        public User NewUser { get; set; }

        public MessageCloseNewUserWindow()
        {

        }
        public MessageCloseNewUserWindow(User user)
        {
            NewUser = user;
        }
    }
}
