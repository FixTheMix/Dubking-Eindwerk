using GalaSoft.MvvmLight;
using DubKing.Utils;
using DubKing.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using DubKing.View;
using System.Windows.Controls;
using System.Collections.Generic;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight.Messaging;
using DubKing.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using DubKing.View.VoiceLibrary;
using System.Diagnostics;
using System.Windows;
using ProgressBar = DubKing.View.DubKingProgress;
using DubKing.Messages;
using DubKing.Controls.ActiveActors;

namespace DubKing.ViewModel
{

    public delegate void CloseWindow();
    

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        #region Fields
        private User _activeUser;
        private ICommand _projectMenuBarCommand;
        private ICommand _activeActorsMenuBarCommand;
        private ICommand _voiceLibraryMenuBarCommand;
        private ICommand _scheduleMenuBarCommand;
        private ICommand _settingsMenuBarCommand;
        private ICommand _loginCommand;
        private ICommand _logoutCommand;
        private ICommand _closeCommand;
        private object _bodyContent;
        private IUserService  _userService;
        User _loginUser;
        bool _registeredOpenProject;
        private ProjectsListView _projectView;
        private ActiveActorsListView _activeActorsListView;

        #endregion

        public event CloseWindow ShutDown;


        #region Methodes
        private void RegisterToOpenProject()
        {
            if (!_registeredOpenProject)
            {
                Messenger.Default.Register<MessageOpen<Project>>(this, OpenProject);
                _registeredOpenProject = true;
            }
        }
        private void UnregisterToOpenProject()
        {
            if (_registeredOpenProject)
            {
                Messenger.Default.Unregister<MessageOpen<Project>>(this);
                _registeredOpenProject = false;
            }
        }
        
        #endregion

        #region Properties
        public string UserName
        {
            get { return _activeUser.UserName; }
            set
            {
                if (_activeUser.UserName == value) return;
                _activeUser.UserName = value;
            }
        }
        public User UserLogin
        {
            get { return _loginUser; }
            set { Set(ref _loginUser, value); }
        }
        public ICommand ProjectMenuBarCommand
        {
            get { return _projectMenuBarCommand ?? (new RelayCommand(OnProjectsMenuBar)); }
        }
        public ICommand ActiveActorsMenuBarCommand
        {
            get { return _activeActorsMenuBarCommand ?? (new RelayCommand(OnActiveActorsMenuBar)); }
        }
        public ICommand VoiceLibraryMenuBarCommand
        {
            get { return _voiceLibraryMenuBarCommand ?? (new RelayCommand(OnVoiceLibraryMenuBar)); }
        }
        public ICommand ScheduleMenuBarCommand
        {

            get { return _scheduleMenuBarCommand ?? (new RelayCommand(OnScheduleMenuBar)); }
        }
        public ICommand SettingsMenuBarCommand
        {
            get { return _settingsMenuBarCommand ?? (new RelayCommand(OnSettingsMenuBar, OnCanSettingsMenuBar)); }
        }
        public ICommand LoginCommand
        {
            get { return _loginCommand ?? (new RelayCommand<PasswordBox>(OnLogin)); }
        }
        public ICommand LogoutCommand
        {
            get { return _loginCommand ?? (new RelayCommand(OnLogout)); }
        }
        public ICommand CloseCommand
        {
            get { return _closeCommand ?? (new RelayCommand(OnClose)); }
        }
        private ProjectsListView ProjectsListView
        {
            get { return _projectView ?? (_projectView = new ProjectsListView()); }
        }
        private ActiveActorsListView ActiveActorsListView
        {
            get { return _activeActorsListView ?? (_activeActorsListView = new ActiveActorsListView()); }
        }
        public bool RememberUser { get; set; }


        public GridPosition ProjectBtnPosition { get; private set; }
        public GridPosition ActiveActorsBtnPosition { get; private set; }
        public GridPosition VoiceLibraryBtnPosition { get; private set; }
        public GridPosition ScheduleBtnPosition { get; private set; }
        public GridPosition SettingsBtnPosition { get; private set; }
        public GridPosition BodyPosition { get; private set; }
        public object BodyContent
        {
            get { return _bodyContent; }
            private set { Set(ref _bodyContent, value); }
        }
        
        #endregion
        #region Constructors
        public MainViewModel(IUserService userService)
        {
            _userService = userService;
            _registeredOpenProject = false;
            _activeUser = new User();
            InitializePositionProperties();
            OnProjectsMenuBar();
            UserLogin = new User();
            _activeUser = _userService.GetRememberedUser();
            Messenger.Default.Register<LogOutMessage>(this, OnLogout);
        }
        #endregion


        #region Navigation
        private void OnProjectsMenuBar()
        {
            //SetProjectsVisible();
            ProjectBtnPosition.SetPosition(3, 0, 1, 1, 0);
            ActiveActorsBtnPosition.SetPosition(2, 0, 1, 3, 90);
            VoiceLibraryBtnPosition.SetPosition(1, 0, 1, 3, 90);
            ScheduleBtnPosition.SetPosition(0, 0, 1, 2, 90);
            SettingsBtnPosition.SetPosition(0, 2, 1, 1, 0);
            BodyPosition.SetPosition(3, 1, 5, 2, 0);
            BodyContent = ProjectsListView;
            RegisterToOpenProject();
        }
        private void OnActiveActorsMenuBar()
        {
            //SetActiveActorsVisible();
            ProjectBtnPosition.SetPosition(7, 1, 1, 2, 90);
            ActiveActorsBtnPosition.SetPosition(2, 0, 2, 1, 0);
            VoiceLibraryBtnPosition.SetPosition(1, 0, 1, 3, 90);
            ScheduleBtnPosition.SetPosition(0, 0, 1, 2, 90);
            SettingsBtnPosition.SetPosition(0, 2, 1, 1, 0);
            BodyPosition.SetPosition(2, 1, 5, 2, 0);
            BodyContent = ActiveActorsListView;
            UnregisterToOpenProject();
            MessengerInstance.Send<LoadActiveActorsMessage>(new LoadActiveActorsMessage());
        }
        private void OnVoiceLibraryMenuBar()
        {
            //SetVoiceLibraryVisable();
            ProjectBtnPosition.SetPosition(7, 1, 1, 2, 90);
            ActiveActorsBtnPosition.SetPosition(6, 1, 1, 2, 90);
            VoiceLibraryBtnPosition.SetPosition(1, 0, 3, 1, 0);
            ScheduleBtnPosition.SetPosition(0, 0, 1, 2, 90);
            SettingsBtnPosition.SetPosition(0, 2, 1, 1, 0);
            BodyPosition.SetPosition(1, 1, 5, 2, 0);
            BodyContent = new VoiceLibraryList();
            UnregisterToOpenProject();
        }
        private void OnScheduleMenuBar()
        {
            //SetScheduleVisible();
            ProjectBtnPosition.SetPosition(7, 1, 1, 2, 90);
            ActiveActorsBtnPosition.SetPosition(6, 1, 1, 2, 90);
            VoiceLibraryBtnPosition.SetPosition(5, 1, 1, 2, 90);
            ScheduleBtnPosition.SetPosition(0, 0, 4, 1, 0);
            SettingsBtnPosition.SetPosition(0, 1, 1, 2, 90);
            BodyPosition.SetPosition(1, 1, 4, 2, 0);
            BodyContent = new Control();
            UnregisterToOpenProject();
        }
        private void OnSettingsMenuBar()
        {
            //SetSettingsVisible();
            ProjectBtnPosition.SetPosition(7, 1, 1, 2, 90);
            ActiveActorsBtnPosition.SetPosition(6, 1, 1, 2, 90);
            VoiceLibraryBtnPosition.SetPosition(5, 1, 1, 2, 90);
            ScheduleBtnPosition.SetPosition(4, 1, 1, 2, 90);
            SettingsBtnPosition.SetPosition(0, 0, 4, 1, 0);
            BodyPosition.SetPosition(0, 1, 4, 2, 0);
            BodyContent = new SettingsPage();
            UnregisterToOpenProject();
        }
        private bool OnCanSettingsMenuBar()
        {
            return _activeUser.SettingsAccess != SettingsModuleAccess.NoAccess;
        }

        private ProjectDetails _projectDetails;
        private void OpenProject(MessageOpen<Project> project)
        {
            int count = project.Project.AutherizedUsers.Where(_ => _.Id == _activeUser.Id).Count();
            if (count > 0)
            {
                //Via messenger naar MainWindow
                if (_projectDetails == null) _projectDetails = new ProjectDetails();
                BodyContent = _projectDetails;
                Messenger.Default.Send<OpenDetails>(new OpenDetails(project.Project));
            }
        }
        #endregion

        private void InitializePositionProperties()
        {
            ProjectBtnPosition = new GridPosition();
            ActiveActorsBtnPosition = new GridPosition();
            VoiceLibraryBtnPosition = new GridPosition();
            ScheduleBtnPosition = new GridPosition();
            SettingsBtnPosition = new GridPosition();
            BodyPosition = new GridPosition();
        }
        
        #region Commands
        private void OnLogin(PasswordBox password)
        {
            if (!_userService.UserExists(UserLogin))
            {
                new ErrorMessageDialog("User does not exist:", "This user does not exist.\nPlease check that the user was spelled correctly or contact the Administrator to create the user.", "Try again").ShowDialog();
            }
            else if (_userService.GetUserByName(UserLogin.UserName).Password != password.Password)
            {
                new ErrorMessageDialog("Password incorrect", "Password was incorrect.\nPlease try again or contact the administrator to reset your password", "Try again").ShowDialog();
            }
            else
            {
                password.Password = string.Empty;
                _activeUser = _userService.GetUserByName(UserLogin.UserName);
                UserLogin = new User();
                _userService.UserLogin(_activeUser, RememberUser);
                RememberUser = false;
                RaisePropertyChanged(nameof(UserName));
            }
        }
        private void OnLogout(LogOutMessage msg)
        {
            OnLogout();
        }
        private void OnLogout()
        {
            OnProjectsMenuBar();
            _userService.UserLogOut();
            _activeUser = new User();
            RaisePropertyChanged(nameof(UserName));
        }
        private void OnClose()
        {
            ShutDown?.Invoke();
        }
        #endregion
    }
}