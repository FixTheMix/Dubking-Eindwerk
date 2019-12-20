using DubKing.Messages;
using DubKing.Model;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DubKing.ViewModel.ActiveActors
{
    public class ActiveActorsViewModel:ViewModelBase
    {
        private readonly IActiveActorService _ActiveActorService;

        private List<ActiveActor> _activeActors;

        public List<ActiveActor> ActiveActors
        {
            get { return _activeActors; }
            set
            {
                _activeActors = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _expandedCommand;
        public ICommand ExpandedCommand { get => _expandedCommand ?? (_expandedCommand = new RelayCommand<ActiveActor>(OnExpanded)); }

        private void OnExpanded(ActiveActor actor)
        {
            _ActiveActorService.LoadProjects(actor);
        }

        public ActiveActorsViewModel(IActiveActorService activeActorService)
        {
            _ActiveActorService = activeActorService;
            MessengerInstance.Register<LoadActiveActorsMessage>(this, LoadActiveActors);
            
        }

        private void LoadActiveActors(LoadActiveActorsMessage msg = null)
        {
            ActiveActors = _ActiveActorService.GetAll().ToList();
        }

        public List<string> SortingOptions { get; } = new List<string>() { "First", "Second", "Third" };

    }
}
