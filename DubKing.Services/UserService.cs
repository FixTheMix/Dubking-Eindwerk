using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DubKing.Model;
using DubKing.Repositories.Contracts;
using DubKing.Services.Contracts;

namespace DubKing.Services
{
    public delegate void ActiveUserChangedEventHandler(User user);
    public class UserService : IUserService
    {
        #region Fields
        private List<User> _user;
        private User _activeUser;
        private readonly IRepository<User> _userRepository;
        private IRememberUserRepository _rememberUserRepository;
        #endregion
        #region events
        public event ActiveUserChangedEventHandler ActiveUserChanged;
        #endregion
        #region properties
        
        #endregion

        public bool ValidateUser(User user)
        {
            int count = (from u in _userRepository.GetAll()
                         where u.UserName == user.UserName && u.Password == user.Password
                         select u).Count();
            if (count == 0)
            {
                return false;
            }
            return true;
        }
        public List<User> GetUsers()
        {
            return _userRepository.GetAll();
        }
        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
        public bool UserExists(User user)
        {
            int count = (from u in _userRepository.GetAll()
                         where u.UserName.ToLower() == user.UserName.ToLower()
                         select u).Count();
            if (count > 0) return true;
            return false;
        }
        public User SaveUser(User user)
        {
           return _userRepository.Create(user);
        }
        public User GetUserByName(string userName)
        {
            foreach (User user in _userRepository.GetAll())
            {
                if (user.UserName == userName)
                {
                    return user;
                }
            }
            return null;
        }
        public int CountUsersSettingsAccess()
        {
            int count = (from u in _user
                         where u.SettingsAccess == SettingsModuleAccess.ReadWrite
                         select u).Count();
            return count;
        }

        public bool UserLogin(User user, bool remember = false)
        {
            //ValidateUser
            //Validate Pasword
            //loginUser
            _activeUser = user;
            ActiveUserChanged?.Invoke(_activeUser);
            if (remember)
            {
                _rememberUserRepository.Save(user);
            }
            else
            {
                _rememberUserRepository.Delete();
            }
            return true;
        }

        public void UserLogOut()
        {
            _rememberUserRepository.Delete();
        }
        public User GetActiveUser()
        {
            return _activeUser;
        }

        public User GetRememberedUser()
        {
            var user = _rememberUserRepository.Get();
            if (user != null)
            {
                _activeUser = user;
                ActiveUserChanged?.Invoke(_activeUser);
            }
            return _activeUser;
        }
        #region Constructor
        public UserService(IRepository<User> userRepository, IRememberUserRepository rememberUserRepository)
        {
            _userRepository = userRepository;
            _rememberUserRepository = rememberUserRepository;
            //CreateAdmin();
        }

        
        #endregion
    }
}
