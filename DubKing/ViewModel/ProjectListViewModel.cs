using System.Collections.ObjectModel;
using DubKing.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using DubKing.Services;
using System.Windows.Controls;
using System.Collections.Generic;
using GalaSoft.MvvmLight.CommandWpf;
using DubKing.View;
using System.Linq;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight.Messaging;
using DubKing.Helpers;
using System;

namespace DubKing.ViewModel
{
    
    public class ProjectListViewModel : ViewModelBase
    {
        #region Fields
        BarViewModel<Project> _selectedProject;
        ObservableCollection<BarViewModel<Project>> _projects;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        List<Control> _mainMenu;
        ICommand _newProject;
        ICommand _deleteProject;
        ICommand _startSearch;
        ICommand _openProjectCommand;
        bool _showList;
        #endregion

       

        #region Properties

        public List<Control> MainMenu
        {
            get { return _mainMenu; }
            private set { _mainMenu = value; }
        }
        public BarViewModel<Project> SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                UndoChanges();
                Set(ref _selectedProject, value);
            }
        }

        private void UndoChanges()
        {
            if (_selectedProject != null && !_selectedProject.Object.IsValid)
            {
                var oldValues = _projectService.Get(_selectedProject.Object.ProjectId);
                _selectedProject.Object.Comment = oldValues.Comment;
                _selectedProject.Object.Customer = oldValues.Customer;
                _selectedProject.Object.Title = oldValues.Title;
            }
        }

        public ObservableCollection<BarViewModel<Project>> Projects
        {
            get { return _projects; }
            set
            {
                Set(ref _projects, value);
            }
        }
        public ICommand NewProjectCommand
        {
            get { return _newProject ?? (_newProject = new RelayCommand(OnNewProject, OnCanNewProject)); }
        }
        public ICommand DeleteProjectCommand
        {
            get { return _deleteProject ?? (_deleteProject = new RelayCommand(OnDeleteProject, OnCanDelete)); }
        }
        public ICommand StartSearchCommand
        {
            get { return _startSearch ?? (_startSearch = new RelayCommand<string>(OnStartSearch)); }
        }
        public ICommand OpenProjectCommand
        {
            get { return _openProjectCommand ?? (_openProjectCommand = new RelayCommand(OnOpenProject, OnCanOpenProject)); }
        }
        #endregion
        #region methods
        private void LoadProjects(User user)
        {
            Projects = new ObservableCollection<BarViewModel<Project>>();
            foreach (Project p in _projectService.GetProjects(user))
            {
                Projects.Add(CreateBarViewModel(p, user));
            }
            BarViewModel<Project>.OpenObjectChanged += SetSelectedBar;
        }

        private void SetSelectedBar(object sender, EventArgs e)
        {
            SelectedProject = BarViewModel<Project>.OpenObject;
        }

        private bool Accessebility(User user)
        {
            return user.ProjectAccess == ProjectModuleAccess.ReadOnly;
        }
        #endregion
        #region Commands
        #region New Project
        private void OnNewProject()
        {
            Messenger.Default.Register<CloseNewProjectWindow>(this, OnCloseNewProjectWindow);
            Messenger.Default.Send<OpenNewProjectWindow>(new OpenNewProjectWindow());
        }
        private bool OnCanNewProject()
        {
            if (_userService.GetActiveUser() != null)
            {
                return _userService.GetActiveUser().ProjectAccess == ProjectModuleAccess.ReadWrite;
            }
            return false;
            
        }

        private void OnCloseNewProjectWindow(CloseNewProjectWindow message)
        {
            Messenger.Default.Unregister<CloseNewProjectWindow>(this);
            if (message.NewProject != null)
            {
                Projects.Add(CreateBarViewModel(message.NewProject, _userService.GetActiveUser()));
            }
        }
        #endregion
        #region Delete Project
        private void OnDeleteProject()
        {
            if (new ConfirmDelete().GetConfirmation(SelectedProject.Object.Customer + " - " + SelectedProject.Object.Title, "project"))
            {
                _projectService.DeleteProject(_selectedProject.Object);
                Projects.Remove(SelectedProject);
            }
        }
        private bool OnCanDelete()
        {
            if (_userService.GetActiveUser() != null && (_userService.GetActiveUser().ProjectAccess == ProjectModuleAccess.ReadWrite))
            {
                return _selectedProject != null;
            }
            return false;
        }
        #endregion
        #region Start Search
        private void OnStartSearch(string input = "")
        {
            var searchResults = from project in _projectService.GetProjects()
                                where (project.Title.ToLower().Contains(input.ToLower()) || project.Customer.ToLower().Contains(input.ToLower())) && project.AutherizedUsers.Contains(_userService.GetActiveUser())
                                select new BarViewModel<Project>(project);
            Projects.Clear();
            foreach (BarViewModel<Project> project in searchResults)
            {
                Projects.Add(project);
            }
        }
        #endregion
        #region Open Project
        private void OnOpenProject()
        {
            Messenger.Default.Send(new MessageOpen<Project>(_selectedProject.Object));
        }
        private bool OnCanOpenProject()
        {
            //er moet een project geselecteerd zijn
            if (_selectedProject != null)
            {
                //het geselecteerde project moet in AuterizedUsers de active gebruiker bezitten
                if (true)
                {
                    if ((from u in _selectedProject.Object.AutherizedUsers
                         where u.Id == _userService.GetActiveUser().Id
                         select u).Count() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion


        #endregion

        #region Contructors
        public ProjectListViewModel(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
            _userService.ActiveUserChanged += LoadProjects;
            CreateMainMenu();
        }
        #endregion
        private void CreateMainMenu()
        {
            var menu = new List<Control>();
            menu.Add(new MenuItem
            {
                Header = "_Open Project",
                Command = OpenProjectCommand
            });
            menu.Add(new MenuItem
            {
                Header = "_New Project",
                Command = NewProjectCommand
            });
            menu.Add(new MenuItem
            {
                Header = "_Import Project",
                //Command = NewUserCommand
                IsEnabled = false
            });

            menu.Add(new Separator());
            menu.Add(new MenuItem
            {
                Header = "_Archive Project",
                //Command = DeleteCommand
                IsEnabled = false
            });
            menu.Add(new MenuItem
            {
                Header = "_Delete Project",
                Command = DeleteProjectCommand
            });
            MainMenu = menu;
        }

        private BarViewModel<Project> CreateBarViewModel(Project project, User activeUser)
        {
            var bar = new BarViewModel<Project>(project);
            bar.IsReadOnly = Accessebility(activeUser);
            bar.ObjectChanged += UpdateProject;
            return bar;
        }
        private void UpdateProject(object obj, EventArgs e)
        {
            _projectService.UpdateProject((Project)obj);
        }


    }
}
