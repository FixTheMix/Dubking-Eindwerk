using DubKing.Helpers;
using DubKing.Messages;
using DubKing.Model;
using DubKing.Services.Contracts;
using DubKing.Utils;
using DubKing.View;
using DubKing.View.Episode;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DubKing.ViewModel
{
    public class ProjectDetailsViewModel:ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;
        private readonly IEpisodeService _episodeService;
        private readonly ILineService _lineService;
        private readonly IProjectTableService _projectTableService;
        private readonly ICharacterService _characterService;
        private readonly IProjectService _projectService;
        private ObservableCollection<BarViewModel<Episode>> _episodes;
        private BarViewModel<Episode> _selectedEpisode;
        private ProjectTable.ProjectTableViewModel _projectTableViewModel;
        private List<Control> _mainMenu;
        Project _project;
        List<UserCheck> _allUsers;
        ObservableCollection<string> _languages;
        private ICommand _ImportEpisodeCommand;
        private ICommand _deleteEpisodeCommand;
        private ICommand _OpenScenarioCommand;
        private Dictionary<int, List<Control>> _menuDictionary;
        private ICommand _setStatus;
        private ICommand _selectAll;

        public ICommand SetStatus { get => _setStatus ?? (_setStatus = new RelayCommand<Episode>(OnSetSatus, IsAuhorized)); }
        public ProjectTable.ProjectTableViewModel ProjectTableViewModel
        {
            get => _projectTableViewModel;
            set => Set(ref _projectTableViewModel, value);
        }
        public Project Object
        {
            get { return _project; }
            set
            {
                Set(ref _project , value);
            }
        }
        public ObservableCollection<string> Languages
        {
            get { return _languages; }
        }
        public List<UserCheck> AllUsers
        {
            get { return _allUsers; } 
        }
        public ObservableCollection<BarViewModel<Episode>> Episodes
        {
            get { return _episodes; }
            set { Set(ref _episodes, value); }
        }
        public BarViewModel<Episode> SelectedEpisode
        {
            get { return _selectedEpisode; }
            set { Set(ref _selectedEpisode, value); }
        }
        public List<Control> MainMenu
        {
            get { return _menuDictionary[SelectedTabIndex]; }
            
        }
        public bool AllAreChecked
        { get => AllUsers.Where(_ => !_.Selected).Count() == 0; }
        public ICommand SelectAll { get => _selectAll ?? (_selectAll = new RelayCommand(OnSelectAll, IsAuthorized)); }
        public ICommand ImportEpisodeCommand
        {
            get { return _ImportEpisodeCommand ?? (_ImportEpisodeCommand = new RelayCommand(OnImportEpisode, IsAuthorized)); }
        }

        private bool IsAuhorized(Episode ep)
        {
            return IsAuthorized();
        }
        private bool IsAuthorized()
        {
            return _activeUser.ProjectAccess == ProjectModuleAccess.ReadWrite;
        }

        public ICommand DeleteEpisodeCommand
        {
            get { return _deleteEpisodeCommand ?? (_deleteEpisodeCommand = new RelayCommand(OnDeleteEpisode, OnCanDeleteEpisode)); }
        }
        public ICommand OpenScenario { get => _OpenScenarioCommand ?? (_OpenScenarioCommand = new RelayCommand<Episode>(OnOpenScenario)); }
        private void OnOpenScenario(Episode obj)
        {
            MessengerInstance.Send<OpenScenarioWindow>(new OpenScenarioWindow(obj));
        }
        private void OnCloseScenario(ClosingScenarioMessage obj)
        {
            _project = _projectTableService.GetProjectTable(_project);
            ProjectTableViewModel.LoadProjectTable(_project);
        }
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                Set(ref _selectedTabIndex, value);
                RaisePropertyChanged(nameof(MainMenu));
            }
        }
        private User _activeUser;

        public User ActiveUser
        {
            get { return _activeUser; }
        }



        public ProjectDetailsViewModel(IProjectService projectService, IProjectTableService projectTableService, ILanguageService languageService, IEpisodeService episodeService, IUserService userService, ICharacterService characterService, ILineService lineService)
        {
            _projectService = projectService;
            _languageService = languageService;
            _userService = userService;
            _userService.ActiveUserChanged += SetActiveUser;
            SetActiveUser(_userService.GetActiveUser());
            _episodeService = episodeService;
            _characterService = characterService;
            _lineService = lineService;
            //_project = _projectTableService.GetProjectTable()
            _projectTableService = projectTableService;
            Messenger.Default.Register<OpenDetails>(this, SetProject);
            CreateMainMenu();
            LoadLanguages();
            MessengerInstance.Register<ClosingScenarioMessage>(this, OnCloseScenario);
        }

        private void SetActiveUser(User user)
        {
            _activeUser = user;
            RaisePropertyChanged(nameof(ActiveUser));
        }
        private void OnSelectAll()
        {
            bool checkTo = !AllAreChecked;
            
            foreach (var item in AllUsers)
            {
                if (item.CanChange)
                {
                    item.SetSelected(checkTo, true);
                }
                switch (item.Selected)
                {
                    case true:
                        _project.AutherizedUsers.Add(item.User);
                        break;
                    case false:
                        _project.AutherizedUsers.Remove(item.User);
                        break;
                }
            }
            UpdateProject(_project, EventArgs.Empty);
            RaisePropertyChanged(nameof(AllAreChecked));
        }
        private void OnSetSatus(Episode obj)
        {
            if (obj.EpStatus < EpStatus.Delivering)
            {
                obj.EpStatus = obj.EpStatus + 1;
            }
            else
            {
                obj.EpStatus = EpStatus.Recording;
            }
            _episodeService.UpdateEpisode(obj);
        }
        public void SetProject(OpenDetails message)
        {//Object of _object met RaissePropertyCHanged??
            Object = _projectTableService.GetProjectTable(message.Project);
            LoadAllUsers();
            LoadEpisodes();
            CreateProjectTableViewModel();
        }
        private void LoadEpisodes()
        {
            if (_episodes != null)
            {
                _episodes.Clear();
            }
            Episodes = new ObservableCollection<BarViewModel<Episode>>(_project.Episodes.Select(e => CreateBarViewModel(e)));
        }
        private void LoadLanguages()
        {
            _languages = new ObservableCollection<string>(_languageService.GetLanguages());
        }
        private void CreateProjectTableViewModel()
        {
            ProjectTableViewModel = new ProjectTable.ProjectTableViewModel(_project, _characterService, _lineService, _episodeService, _projectTableService, _userService);
        }
        private BarViewModel<Episode> CreateBarViewModel(Episode episode)
        {
            var barViewModel = new BarViewModel<Episode>(episode);
            barViewModel.ObjectChanged += UpdateEpisode;
            return barViewModel;
        }
        private void UpdateEpisode(object sender, EventArgs e)
        {
            var episode = (Episode)sender;
            _episodeService.UpdateEpisode(episode);
        }
        private void LoadAllUsers()
        {
            _allUsers = new List<UserCheck>();
            var dbUsers = _userService.GetUsers();
            User activeUser = _userService.GetActiveUser();
            for (int i = 0; i < dbUsers.Count; i++)
            {
                var tempUser = new UserCheck(dbUsers[i]);
                if (activeUser == dbUsers[i])
                {
                    tempUser.Selected = true;
                    tempUser.CanChange = false;
                }
                else
                {
                    var count = _project.AutherizedUsers.Where(_ => _.Id == dbUsers[i].Id).Count();
                    tempUser.Selected = count != 0;
                    //tempUser.Selected = _project.AutherizedUsers.Any(_ => _.Id == dbUsers[i].Id);
                }
                tempUser.UserAdded += UpdateUsers;
                _allUsers.Add(tempUser);
            }
            RaisePropertyChanged(nameof(AllUsers));
        }
        private void UpdateUsers(object obj, EventArgs e)
        {
            var userCheck = (UserCheck)obj;
            switch (userCheck.Selected)
            {
                case true:
                    _project.AutherizedUsers.Add(userCheck.User);
                    break;
                case false:
                    _project.AutherizedUsers.Remove(userCheck.User);
                    break;
            }
            RaisePropertyChanged(nameof(AllAreChecked));
            UpdateProject(_project, EventArgs.Empty);
        }
        
        private void CreateMainMenu()
        {
            _menuDictionary = new Dictionary<int, List<Control>>();
            var projectTableMenu = new List<Control>();
            projectTableMenu.Add(new MenuItem
            {
                Header = "E_xport Project Table",
                IsEnabled = false
            });
            projectTableMenu.Add(new MenuItem
            {
                Header = "Import Episode",
                Command = ImportEpisodeCommand
            });

            _menuDictionary.Add(1, new List<Control>());

            _menuDictionary.Add(0, projectTableMenu);

            var episodeListMenu = new List<Control>();
            episodeListMenu.Add(new MenuItem
            {
                Header = "_Export Title List",
                //Command = OpenProjectCommand
                IsEnabled = false
            });
            episodeListMenu.Add(new MenuItem
            {
                Header = "Import Episode",
                Command = ImportEpisodeCommand
            });
            episodeListMenu.Add(new Separator());
            episodeListMenu.Add(new MenuItem
            {
                Header = "Delete Episode",
                Command = DeleteEpisodeCommand
            });
            _menuDictionary.Add(2,episodeListMenu);
        }
        private void OnImportEpisode()
        {
            Messenger.Default.Register<EpisodeAdded>(this, AddEpisode);
            Messenger.Default.Send(new OpenImportEpisodeWindow(_project));
        }
        private void AddEpisode(EpisodeAdded obj)
        {
            MessengerInstance.Unregister<EpisodeAdded>(this);
            if (obj.Episode != null)
            {
                _project.AddEpisode(obj.Episode);
                ProjectTableViewModel.LoadProjectTable(_project);
                _episodes = new ObservableCollection<BarViewModel<Episode>>(_project.Episodes.OrderBy((_)=> _.CustomCode).ThenBy(_=>_.Number).Select(_ => CreateBarViewModel(_)));
                RaisePropertyChanged(nameof(Episodes));
            }
        }
        private void OnDeleteEpisode()
        {
            var confirmWindow = new ConfirmDelete();
            string episodeName = _selectedEpisode.Object.Project.Title + " - " + _selectedEpisode.Object.Number + " - " + _selectedEpisode.Object.Title;
            if (confirmWindow.GetConfirmation(episodeName, "Episode"))
            {
                _episodeService.DeleteEpisode(_selectedEpisode.Object);
                _project.RemoveEpisode(_selectedEpisode.Object);
                _episodes.Remove(_selectedEpisode);
                _projectTableService.GetProjectTable(_project);
                _projectTableViewModel.LoadProjectTable(_project);
            }
        }
        private bool OnCanDeleteEpisode()
        {
            if (!IsAuthorized())
            {
                return false;
            }
            return _selectedEpisode != null;
        }
        private void RegisterToChangeEvent()
        {
            _project.HasChanged += UpdateProject;
        }
        private void UpdateProject(object obj, EventArgs e)
        {
            _projectService.UpdateProject((Project)obj);
        }
    }
}
