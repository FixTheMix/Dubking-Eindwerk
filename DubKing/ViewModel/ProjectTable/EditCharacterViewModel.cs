using DubKing.Messages;
using DubKing.Model;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DubKing.ViewModel.ProjectTable
{
    public class EditCharacterViewModel : ViewModelBase
    {
        private readonly ICharacterService _characterService;
        private Character _character;
        private Character _characterCopy;
        private int _selectedTab;
       
        private ObservableCollection<Character> _projectCharacters;
        private ObservableCollection<Episode> _episodes;

        public Character Character
        {
            get
            {
                return _character;
            }
        }
        public Character CharacterCopy
        {
            get { return _characterCopy; }
            set { Set(ref _characterCopy, value); }
        }
        public int SelectedTab { get => _selectedTab;
            set => _selectedTab = value; }
        public string Seperator { get; set; }





        public ObservableCollection<Character> ProjectCharacters
        {
            get => _projectCharacters;
        }
        public ObservableCollection<Episode> Episodes
        {
            get => _episodes;
        }
        public bool BetweenRange
        {
            get; set;
        }

        public EditCharacterViewModel(ICharacterService characterService)
        {
            _characterService = characterService;
            MessengerInstance.Register<SendCharacterToEdit>(this, LoadCharacter);
        }


        private void LoadCharacter(SendCharacterToEdit msg)
        {
            _character = msg.Character;
            CharacterCopy = new Character(_character);
            LoadCharacterList();
        }
        
        private void SaveComment()
        {
            _character.Comment = _characterCopy.Comment;
        }
        private void SaveRename()
        {
            if (!BetweenRange)
            {
                _character.Name = _characterCopy.Name;
            }
        }
        private void LoadCharacterList()
        {
            _projectCharacters = new ObservableCollection<Character>(_characterService.Get(_character.Project));
        }
    }
}
