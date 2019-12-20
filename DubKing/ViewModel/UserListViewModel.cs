using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using DubKing.View;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight.Messaging;
using DubKing.Helpers;

namespace DubKing.ViewModel
{
    public class UserListViewModel:ViewModelBase
    {
        #region Fields
        ObservableCollection<BarViewModel<User>> _users;
        private BarViewModel<User> _selectedUser;
        private List<Control> _mainMenu;
       
        IUserService _userService;
        ICommand _deleteCommand;
        ICommand _NewUserCommand;
        


           
        #endregion

        #region Properties
        public ObservableCollection<BarViewModel<User>> Users
        {
            get { return _users; }
            set
            {
                Set(ref _users, value);
            }
        }
        public BarViewModel<User> SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                Set(ref _selectedUser, value);
            }
        }
        public List<Control> MainMenu
        {
            get { return _mainMenu; }
            private set { _mainMenu = value; }
        }
        
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (new RelayCommand(OnDeleteUser, CanDeleteUser)); }
        }
        public ICommand NewUserCommand
        {
            get { return _NewUserCommand ?? (new RelayCommand(OnNewUser)); }
        }
       
        #endregion

        #region Commands
        private void OnDeleteUser()
        {
            if (new ConfirmDelete().GetConfirmation(SelectedUser.Object.UserName , "user"))
            {
                _userService.DeleteUser(_selectedUser.Object);
                Users.Remove(SelectedUser);
            }
        }
        private bool CanDeleteUser()
        {
            if (SelectedUser == null)
            {
                return false;
            }
            return true;
        }

        private void OnNewUser()
        {
            Messenger.Default.Register<MessageCloseNewUserWindow>(this, AddUser);
            Messenger.Default.Send<MessageOpenNewUserWindow>(new MessageOpenNewUserWindow());
        }

       
        #endregion

        #region Methodes
        private void AddUser(MessageCloseNewUserWindow user)
        {
            if (user.NewUser != null)
            {
                CreateViewModel(user.NewUser);
            }
            Messenger.Default.Unregister<MessageCloseNewUserWindow>(this);
        }
        private void LoadUsers()
        {
            _users = new ObservableCollection<BarViewModel<User>>();
            foreach (User user in _userService.GetUsers())
            {
                CreateViewModel(user);
            }
        }

        private void CreateViewModel(User user)
        {
            var barVM = new BarViewModel<User>(user);
            barVM.ObjectChanged += UpdateUser;
            _users.Add(barVM);
        }

        private void CreateMainMenu()
        {
            var menu = new List<Control>();
            menu.Add(new MenuItem
            {
                Header = "_New User",
                Command = NewUserCommand
            });
            
            menu.Add(new Separator());
            menu.Add(new MenuItem
            {
                Header = "Delete User",
                Command = DeleteCommand
            });
            MainMenu = menu;
        }
        #endregion

        #region Constructor
        public UserListViewModel(IUserService userService)
        {
            _userService = userService;
            LoadUsers();
            CreateMainMenu();
        }
        private void UpdateUser(object user, EventArgs e)
        {
            //int count = (from u in _users
            //             where u.Object.SettingsAccess == SettingsModuleAccess.ReadWrite
            //             select u).Count();
            //var updateUser = (User)user;
            //if (count > 0)
            //{
            //    _userService.UpdateUser((User)user);
            //}
            //else
            //{
            //    MessageBox.Show("At least One user needs access to settings module!");
            //}

            var input = (User)user;
            input.ValidateSettingsAccess(_userService.GetUsers().ToArray());
            if (input.IsValid)
            {
                _userService.UpdateUser(input);
            }
            
        }
        
        #endregion
       

    }
}
