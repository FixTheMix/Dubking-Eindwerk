using DubKing.Helpers;
using DubKing.Model;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DubKing.ViewModel.UserViewModels
{
    public class NewUserViewModel:ViewModelBase
    {
        private IUserService _userService;
        ICommand _saveUserCommand;
        ICommand _cancelCommand;
        public User User { get; set; }
        public ICommand SaveUserCommand { get { return _saveUserCommand ?? (_saveUserCommand = new RelayCommand(OnSaveNewUser)); } }
        public ICommand CancelCommand { get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(OnCancel)); } }

        private void OnSaveNewUser()
        {
            if (User.UserName != null || User.UserName != string.Empty)
            {
                User.IsUnique = !_userService.UserExists(User);
            }
            if (User.IsValid)
            {
                MessengerInstance.Send<MessageCloseNewUserWindow>(new MessageCloseNewUserWindow(_userService.SaveUser(User)));
                User = new User();
            }
        }
        private void OnCancel()
        {
            User = new User();
            MessengerInstance.Send<MessageCloseNewUserWindow>(new MessageCloseNewUserWindow());
        }
        public NewUserViewModel(IUserService userService)
        {
            _userService = userService;
            User = new User();
        }
    }
}
