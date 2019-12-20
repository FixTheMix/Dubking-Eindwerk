using DubKing.Helpers;
using DubKing.Model;
using DubKing.Services;
using DubKing.Services.Contracts;
using DubKing.Utils;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DubKing.ViewModel
{

    public class NewProjectViewModel : ViewModelBase
    {
        #region Fields
        private Project _object;
        ObservableCollection<UserCheck> _usersList;
        private IUserService _userService;
        private ILanguageService _languageService;
        private IProjectService _projectService;
        private ICommand _SaveProjectCommand;
        private ICommand _cancelCommand;
        List<UserCheck> _allUsers;
        ObservableCollection<String> _languages;
        #endregion

        #region Properties
        public Project Object
        {
            get { return _object; }
            set { Set(ref _object, value); }
        }
        public ObservableCollection<string> Languages
        {
            get { return _languages; }
        }
        public List<UserCheck> AllUsers
        {
            get { return _allUsers; }
        }
        public ObservableCollection<UserCheck> UsersList
        {
            get { return _usersList; }
            set { Set(ref _usersList, value); }
        }
        public ICommand SaveProjectCommand
        {
            get { return _SaveProjectCommand ?? (_SaveProjectCommand = new RelayCommand(OnSaveProject)); }
        }
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand(OnCancelCommand));
            }
        }
        public bool AllAreChecked
        { get => AllUsers.Where(_ => !_.Selected).Count() == 0; }

        private ICommand _selectAll;
        public ICommand SelectAll { get => _selectAll ?? (_selectAll = new RelayCommand(OnSelectAll)); }

        private void OnSelectAll()
        {
            bool checkTo = !AllAreChecked;
            
            foreach (var item in AllUsers)
            {
                if (item.CanChange)
                {
                    item.Selected = checkTo;
                }
            }
        }
        #endregion

        public NewProjectViewModel(IUserService userService, IProjectService projectService, ILanguageService languageService)
        {
            _projectService = projectService;
            _userService = userService;
            _languageService = languageService;
            ResetObject();
            LoadLanguages();
        }

        private void LoadLanguages()
        {
            _languages = new ObservableCollection<string>(_languageService.GetLanguages());
        }

        private void OnSaveProject()
        {
            var projects = _projectService.GetProjects();
            if (projects.Where(p => p.Title == _object.Title && p.Customer == _object.Customer).Count() != 0)
            {
                _object.IsUnique = false;
            }
            if (_object.IsValid)
            {
                var newProject = _projectService.SaveProject(_object);
                ResetObject();
                Messenger.Default.Send(new CloseNewProjectWindow(newProject));
            }
        }
        private void OnCancelCommand()
        {
            ResetObject();
            Messenger.Default.Send(new CloseNewProjectWindow());
        }

        private void ResetObject()
        {
            _object = new Project();
            LoadAllUsers();
            LoadLanguages();
        }

        private void LoadAllUsers()
        {
            _allUsers = new List<UserCheck>();
            foreach (User user in _userService.GetUsers())
            {
                var tempUser = new UserCheck(user);
                if (_userService.GetActiveUser() == user)
                {
                    tempUser.Selected = true;
                    tempUser.CanChange = false;
                    UpdateUsers(tempUser, EventArgs.Empty);
                }
                else
                {
                    tempUser.Selected = _object.AutherizedUsers.Contains(user);
                }
                tempUser.UserAdded += UpdateUsers;
                _allUsers.Add(tempUser);
            }
        }
        private void UpdateUsers(object obj, EventArgs e)
        {
            
            var userCheck = (UserCheck)obj;
            switch (userCheck.Selected)
            {
                case true:
                    _object.AutherizedUsers.Add(userCheck.User);
                    break;
                case false:
                    _object.AutherizedUsers.Remove(userCheck.User);
                    break;
            }
            RaisePropertyChanged(nameof(AllAreChecked));
        }
    }
}
