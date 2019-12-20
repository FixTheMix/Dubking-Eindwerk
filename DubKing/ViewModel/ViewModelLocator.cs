/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DubKing"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using DubKing.Services;
using DubKing.Services.Contracts;
using DubKing.Utils;
using DubKing.ViewModel.ActiveActors;
using DubKing.ViewModel.EpisodeView;
using DubKing.ViewModel.ProjectTable;
using DubKing.ViewModel.Scenario;
using DubKing.ViewModel.UserViewModels;
using DubKing.ViewModel.VoiceLibrary;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace DubKing.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ServicesModule.RegisterTypes();
            SimpleIoc.Default.Register<NewUserViewModel>();
            SimpleIoc.Default.Register<NewProjectViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<UserListViewModel>();
            SimpleIoc.Default.Register<ProjectListViewModel>();
            SimpleIoc.Default.Register<VoiceLibraryListViewModel>(true);
            SimpleIoc.Default.Register<ImportEpisodeViewModel>(true);
            SimpleIoc.Default.Register<ProjectDetailsViewModel>();
            SimpleIoc.Default.Register<NewVoiceTalentViewModel>();
            SimpleIoc.Default.Register<ProjectTableViewModel>();
            SimpleIoc.Default.Register<EditCharacterViewModel>();
            SimpleIoc.Default.Register<ScenarioViewModel>();
            SimpleIoc.Default.Register<ActiveActorsViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public UserListViewModel Users
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserListViewModel>();
            }
        }
        public ProjectListViewModel Projects
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectListViewModel>();
            }
        }
        public VoiceLibraryListViewModel VoiceTalents
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VoiceLibraryListViewModel>();
            }
        }
        public NewProjectViewModel NewProject
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewProjectViewModel>();
            }
        }
        public ProjectDetailsViewModel ProjectDetails
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectDetailsViewModel>();
            }
        }
        public ImportEpisodeViewModel ImportEpisode
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImportEpisodeViewModel>();
            }
        }
        public NewUserViewModel NewUser
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewUserViewModel>();
            }
        }
        public NewVoiceTalentViewModel NewVoiceTalent
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewVoiceTalentViewModel>();
            }
        }
        public ProjectTableViewModel ProjectTable
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProjectTableViewModel>();
            }
        }
        public EditCharacterViewModel EditCharacter
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditCharacterViewModel>();
            }
        }
        public ScenarioViewModel Scenario
        {
            get => ServiceLocator.Current.GetInstance<ScenarioViewModel>();
        }
        public ActiveActorsViewModel ActiveActors
        {
            get => ServiceLocator.Current.GetInstance<ActiveActorsViewModel>();
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}