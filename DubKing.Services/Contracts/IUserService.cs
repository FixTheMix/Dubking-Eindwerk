using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Services.Contracts
{
    public interface IUserService
    {
        event ActiveUserChangedEventHandler ActiveUserChanged;
        
        bool ValidateUser(User user);
        List<User> GetUsers();
        void DeleteUser(User user);
        bool UserExists(User user);
        User SaveUser(User user);
        User GetUserByName(string userName);
        int CountUsersSettingsAccess();
        void UpdateUser(User user);
        bool UserLogin(User user, bool remember = false);
        void UserLogOut();
        User GetActiveUser();
        User GetRememberedUser();

    }
}
