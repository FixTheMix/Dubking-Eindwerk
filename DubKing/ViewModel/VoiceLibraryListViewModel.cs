using System.Collections.ObjectModel;
using DubKing.Model;
using GalaSoft.MvvmLight;
using DubKing.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using DubKing.Services.Contracts;
using System;
using System.Windows.Controls;
using System.Collections.Generic;
using DubKing.Messages;
using System.Linq;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;

namespace DubKing.ViewModel
{
    public class VoiceLibraryListViewModel : ViewModelBase
    {
        private readonly IVoiceTalentService _voiceTalentService;
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;
        private List<BarViewModel<VoiceTalent>> _voiceTalents;
        private ObservableCollection<BarViewModel<VoiceTalent>> _filteredVoiceTalents;
        private BarViewModel<VoiceTalent> _selectedVoice;
        private ICommand _showDetailsCommand;
        private ICommand _newVoiceTalentCommand;
        private ICommand _deleteCommand;
        private ICommand _exportCommand;
        private bool _showDetails;
        private List<Control> _mainMenu;
        private string _fullName;
        private bool _genderSelected;
        private bool _ageSelected;
        private bool _keyWordSelected;
        private bool _ratingSelected;
        private bool _languageSelected;
        private ObservableCollection<SelectableGender> _gender;
        private int _minAge;
        private int _maxAge;
        private ObservableCollection<VLKeyword> _vLKeywords;
        private int _rating;
        private string _language;
        private ObservableCollection<int> _posibleAge;
        private ObservableCollection<VLKeyword> _posibleVLKeywords;
        private ObservableCollection<string> _possibleLanguages;
        private ICommand _clearSearchCommand;
        private ICommand _startSearchCommand;
        private Character _selectedCharacter;
        private ICommand _selectActorCommand;
        private ICommand _clearActorCommand;
        private List<string> _languages;
        private ICommand _NewKeyword;
        private IEnumerable<VLKeyword> _selectedKeywords;


        private ICommand _deleteKeyword;
        public ICommand DeleteKeyword { get => _deleteKeyword ?? (_deleteKeyword = new RelayCommand(OnDeleteKeyword)); }

        private void OnDeleteKeyword()
        {
            var msg = new DeleteKeywordMessage(_vLKeywords);
            MessengerInstance.Send(msg);
            if (!msg.IsCanceled&& msg.SelectedKeyword != null)
            {
                _voiceTalentService.DeleteVlKeyword(msg.SelectedKeyword);
            }
            VLKeywords = new ObservableCollection<VLKeyword>(_voiceTalentService.GetVLKeywords());
        }

        #region Properties

        private ICommand _dragEnter;
        public ICommand DragEnter { get => _dragEnter ?? (_dragEnter = new RelayCommand<DragEventArgs>(OnDragEnter)); }

        private ICommand _PhotoDrop;
        public ICommand PhotoDrop { get => _PhotoDrop ?? (_PhotoDrop = new RelayCommand<DragEventArgs>(OnPhotoDrop)); }

        private void OnDragEnter(DragEventArgs args)
        {
            if (IsValidDropFile(args))
            {
                args.Effects = DragDropEffects.Copy;
            }
            else
            {
                args.Effects = DragDropEffects.None;
            }

            args.Handled = true;
        }
        private void OnPhotoDrop(DragEventArgs args)
        {
            if (IsValidDropFile(args))
            {
                var filenames = args.Data.GetData(DataFormats.FileDrop, true) as string[];
                var originalSource = args.OriginalSource as Image;
                var vt = originalSource.Tag as VoiceTalent;
                _voiceTalentService.SetPicture(filenames[0], vt);
            }
        }

        private static bool IsValidDropFile(DragEventArgs args)
        {
            if (args.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                var fileNames = args.Data.GetData(DataFormats.FileDrop, true) as string[];
                // Check for a single file or folder.
                if (fileNames?.Length is 1)
                {
                    // Check for a file (a directory will return false).
                    if (File.Exists(fileNames[0]))
                    {
                        // At this point we know there is a single file.
                        //MessageBox.Show(fileNames[0]);
                        if (Path.GetExtension(fileNames[0]) == ".bmp" || Path.GetExtension(fileNames[0]) == ".png" || Path.GetExtension(fileNames[0]) == ".jpg")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<Control> MainMenu
        {
            get { return _mainMenu; }
            private set { _mainMenu = value; }
        }
        public BarViewModel<VoiceTalent> SelectedVoice
        {
            get { return _selectedVoice; }
            set
            {
                Set(ref _selectedVoice, value);
            }
        }
        public ICommand ShowDetailsCommand { get { return _showDetailsCommand ?? (new RelayCommand(OnShowDetails)); } }
        public bool ShowDetails
        {
            get { return _showDetails; }
            set { Set(ref _showDetails, value); }
        }
        public List<BarViewModel<VoiceTalent>> VoiceTalents
        {
            get { return _voiceTalents; }
            set
            {
                Set(ref _voiceTalents, value);
            }
        }
        public ObservableCollection<BarViewModel<VoiceTalent>> FilteredVoiceTalents
        {
            get { return _filteredVoiceTalents; }
            set
            {
                Set(ref _filteredVoiceTalents, value);
            }
        }
        public List<string> Suggestions { get; set; }
        public ICommand NewVoiceTalentCommand { get { return _newVoiceTalentCommand ?? (new RelayCommand(OnNewVoiceTalent, OnCanNewVoiceTalent)); } }
        public ICommand DeleteCommand { get { return _deleteCommand ?? (new RelayCommand(OnDelete, OnCanDelete)); } }
        public string FullName
        {
            get { return _fullName; }
            set { Set(ref _fullName, value); }
        }
        public bool GenderSelected
        {
            get { return _genderSelected; }
            set { Set(ref _genderSelected, value); }
        }
        public bool AgeSelected
        {
            get { return _ageSelected; }
            set { Set(ref _ageSelected, value); }
        }
        public bool KeywordSelected
        {
            get { return _keyWordSelected; }
            set { Set(ref _keyWordSelected, value); }
        }
        public bool RatingSelected
        {
            get { return _ratingSelected; }
            set { Set(ref _ratingSelected, value); }
        }
        public bool LanguageSelected
        {
            get { return _languageSelected; }
            set { Set(ref _languageSelected, value); }
        }
        public ObservableCollection<SelectableGender> Gender
        {
            get { return _gender; }
            set { Set(ref _gender, value); }
        }
        public int MinAge
        {
            get { return _minAge; }
            set { Set(ref _minAge, value); }
        }
        public int MaxAge
        {
            get { return _maxAge; }
            set { Set(ref _maxAge, value); }
        }
        public ObservableCollection<VLKeyword> VLKeywords
        {
            get { return _vLKeywords; }
            set { Set(ref _vLKeywords, value); }
        }
        public int Rating
        {
            get { return _rating; }
            set { Set(ref _rating, value); }
        }
        public string Language
        {
            get { return _language; }
            set { Set(ref _language, value); }
        }
        public IEnumerable<int> PosibleAge
        {
            get { return _voiceTalents.Select(v => v.Object.Age).Distinct().OrderBy(_=>_); }
        }
        public IEnumerable<VLKeyword> SelectedKeywords { get => _selectedKeywords; set => _selectedKeywords = value; }
        public ObservableCollection<string> PosibleLanguages
        {
            get { return _possibleLanguages; }
            set { Set(ref _possibleLanguages, value); }
        }
        public ICommand ClearSeach
        {
            get { return _clearSearchCommand ?? (new RelayCommand(OnClearSearch)); }
        }
        public ICommand StartSearch
        {
            get { return _startSearchCommand ?? (new RelayCommand(OnStartSearch)); }
            set { Set(ref _startSearchCommand, value); }
        }
        public ICommand SelectActor
        {
            get { return _selectActorCommand ?? (_selectActorCommand = new RelayCommand(OnSelectActor, IsAuthorizedForProjects)); }
        }
        public ICommand ClearActor
        {
            get { return _clearActorCommand ?? (_clearActorCommand = new RelayCommand(OnClearActor, IsAuthorizedForProjects)); }
        }
        public ICommand NewKeyword { get => _NewKeyword ?? (_NewKeyword = new RelayCommand(OnNewKeyword)); }
        #endregion

        private User _activeUser;

        public User ActiveUser
        {
            get { return _activeUser; }
        }


        private bool IsAuthorizedForProjects()
        {
            return _activeUser.ProjectAccess == ProjectModuleAccess.ReadWrite;
        }
        
        private void SetActiveUser(User user)
        {
            _activeUser = user;
            RaisePropertyChanged(nameof(ActiveUser));
        }
        

        


        #region Constructors
        public VoiceLibraryListViewModel(IVoiceTalentService voiceTalentService, IUserService userService, ILanguageService languageService)
        {
            _voiceTalentService = voiceTalentService;
            _userService = userService;
            _userService.ActiveUserChanged += SetActiveUser;
            SetActiveUser(_userService.GetActiveUser());
            _languageService = languageService;
            LoadVoiceTalents();
            CreateMainMenu();
            _filteredVoiceTalents = new ObservableCollection<BarViewModel<VoiceTalent>>(_voiceTalents);
            Gender = new ObservableCollection<SelectableGender>();
            foreach (Gender item in Enum.GetValues(typeof(Gender)))
            {
                Gender.Add(new SelectableGender() { Gender = item });
            }
            //PosibleAge = new ObservableCollection<int>();
            VLKeywords = new ObservableCollection<VLKeyword>(_voiceTalentService.GetVLKeywords());
            //PosibleKeywords = new ObservableCollection<VLKeyword>();
            //SelectedKeywords = new ObservableCollection<VLKeyword>();
            PosibleLanguages = new ObservableCollection<string>(_languageService.GetLanguages().ToList());
            MessengerInstance.Register<SelectActor>(this, SetCharacter);
            if (PosibleAge.Count() != 0)
            {
                MinAge = PosibleAge.Min();
                MaxAge = PosibleAge.Max();
            }
        }
        #endregion

        #region Commands
        #region New VoiceTalent
        private void OnNewVoiceTalent()
        {
            MessengerInstance.Register<CloseNewVoiceTalentWindow>(this, SaveNewVoiceTalent);
            MessengerInstance.Send<OpenNewVoiceTalentWindow>(new OpenNewVoiceTalentWindow());
        }
        private bool OnCanNewVoiceTalent()
        {
            return !Accessebility(_userService.GetActiveUser());
        }
        private void SaveNewVoiceTalent(CloseNewVoiceTalentWindow message)
        {
            if (message.IsSaved)
            {
                _voiceTalentService.SetPicture(message.NewVoiceTalent.Picture, message.NewVoiceTalent);
                var newVoiceTalent = _voiceTalentService.Create(message.NewVoiceTalent);
                VoiceTalents.Add(CreateBarViewModel(newVoiceTalent, _userService.GetActiveUser()));
                OnStartSearch();
            }
            MessengerInstance.Unregister<CloseNewVoiceTalentWindow>(this);
        }
        #endregion
        #region Delete
        private void OnDelete()
        {
            var confirmation = new ConfirmDeleteMessage();
            confirmation.VTName = _selectedVoice.Object.FullName;
            MessengerInstance.Send<ConfirmDeleteMessage>(confirmation);
            if (confirmation.CanDelete)
            {
                _voiceTalentService.Delete(_selectedVoice.Object);
                _voiceTalents.Remove(_selectedVoice);
                _filteredVoiceTalents.Remove(_selectedVoice);
                _selectedVoice = null;
            }
        }
        private bool OnCanDelete()
        {
            return _selectedVoice != null;
        }
        private void OnNewKeyword()
        {
            var msg = new NewKeywordMessage(_vLKeywords);
            MessengerInstance.Send<NewKeywordMessage>(msg);
            if (msg.IsSaved && msg.IsValid)
            {
                _voiceTalentService.CreateKeyword(msg.Keyword);
            }
            VLKeywords = new ObservableCollection<VLKeyword>(_voiceTalentService.GetVLKeywords());
        }
        #endregion
        #region Export Voice Library

        #endregion
        #region Clearsearch
        private void OnClearSearch()
        {
            FullName = string.Empty;
            GenderSelected = false;
            AgeSelected = false;
            KeywordSelected = false;
            RatingSelected = false;
            LanguageSelected = false;
            foreach (var g in Gender)
            {
                g.IsSelected = false;
            }
            if (PosibleAge.Count() != 0)
            {
                MinAge = PosibleAge.Min();
                MaxAge = PosibleAge.Max();
            }
            Rating = 0;
            Language = string.Empty;
            OnStartSearch();
        }
        #endregion
        #region Start Search
        private void OnStartSearch()
        {
            var filter = FilterFullName(_voiceTalents);
            if (_genderSelected)
            {
                filter = FilterGender(filter);
            }
            if (_ageSelected)
            {
                filter = FilterAge(filter);
            }
            if (_keyWordSelected)
            {
                filter = FilterKeywords(filter);
            }
            if (_ratingSelected)
            {
                filter = FilterRating(filter);
            }
            if (_languageSelected)
            {
                filter = FilterLanguage(filter);
            }
            FilteredVoiceTalents = new ObservableCollection<BarViewModel<VoiceTalent>>(filter.OrderBy(v => v.Object.FullName));
        }


        #endregion
        #region Select Actor
        private void OnSelectActor()
        {
            if (_selectedVoice != null)
            {
                _selectedCharacter.VoiceTalent = _selectedVoice.Object;
                MessengerInstance.Send<CloseSelectActorWindow>(new CloseSelectActorWindow());
            }
        }
        #endregion
        #region Clear Actor
        private void OnClearActor()
        {
            _selectedCharacter.VoiceTalent = new VoiceTalent();
            MessengerInstance.Send<CloseSelectActorWindow>(new CloseSelectActorWindow());
        }
        #endregion
        #endregion

        #region Methodes
        #region Filters
        private IList<BarViewModel<VoiceTalent>> FilterFullName(IList<BarViewModel<VoiceTalent>> list)
        {
            if (!string.IsNullOrEmpty(FullName))
            {
                return (from l in list
                        where l.Object.FullName.Contains(FullName)
                        select l).ToList();
            }
            return list;
        }
        private IList<BarViewModel<VoiceTalent>> FilterGender(IList<BarViewModel<VoiceTalent>> list)
        {
            var genders = _gender.Where(g => g.IsSelected).Select(g => g.Gender).ToList();
            return list.Where(l => genders.Contains(l.Object.Gender)).ToList();
        }
        private IList<BarViewModel<VoiceTalent>> FilterAge(IList<BarViewModel<VoiceTalent>> list)
        {
            return list.Where(l => l.Object.Age >= MinAge && l.Object.Age <= MaxAge).ToList();
        }
        private IList<BarViewModel<VoiceTalent>> FilterRating(IList<BarViewModel<VoiceTalent>> filter)
        {
            return filter.Where(l => l.Object.Rating >= Rating).ToList();
        }
        private IList<BarViewModel<VoiceTalent>> FilterLanguage(IList<BarViewModel<VoiceTalent>> list)
        {
            return list.Where(l => l.Object.Language == Language).ToList();
        }
        private IList<BarViewModel<VoiceTalent>> FilterKeywords(IList<BarViewModel<VoiceTalent>> list)
        {
            return list.Where(l => l.Object.Keywords.Select(_=> _.KeywordId).Intersect(_selectedKeywords.Select(_=>_.KeywordId)).Count() == _selectedKeywords.Count()).ToList();
        }
        #endregion
        private void LoadVoiceTalents()
        {
            _vLKeywords = new ObservableCollection<VLKeyword>(_voiceTalentService.GetVLKeywords());
            _voiceTalents = new List<BarViewModel<VoiceTalent>>();
            var activeUser = _userService.GetActiveUser();
            foreach (VoiceTalent vt in _voiceTalentService.GetAll().AsParallel())
            {
                var keywords = _vLKeywords.Where(_ => vt.Keywords.Select(vtk => vtk.KeywordId).Contains(_.KeywordId)).ToList();
                vt.Keywords = keywords;
                _voiceTalents.Add(CreateBarViewModel(vt, activeUser));
            }
            _voiceTalents = _voiceTalents.OrderBy(v => v.Object.FullName).ToList();
            Suggestions = VoiceTalents.Select(v => v.Object.FullName).ToList();
            
        }
        private BarViewModel<VoiceTalent> CreateBarViewModel(VoiceTalent voiceTalent, User activeUser)
        {
            var bar = new BarViewModel<VoiceTalent>(voiceTalent);
            bar.IsReadOnly = Accessebility(activeUser);
            bar.ObjectChanged += UpdateVoiceTalent;
            return bar;
        }
        /// <summary>
        /// Return true when active user had readOnly Access to voiclibrary module
        /// </summary>
        /// <param name="user">User to check accassebility</param>
        /// <returns>Return true when user had readOnly Access to voiclibrary module</returns>
        private bool Accessebility(User user)
        {
            if (user == null)
            {
                return false;
            }
            return user.VoiceLibraryAccess == ModuleAccess.ReadOnly;
        }
        private void UpdateVoiceTalent(object obj, EventArgs e)
        {
            var vt = (VoiceTalent)obj;
            if (vt.IsValid)
            {
                _voiceTalentService.Update(vt);
                base.RaisePropertyChanged(nameof(this.VoiceTalents));
            }
        }
        private void OnShowDetails()
        {
            ShowDetails = !ShowDetails;
        }
        private void CreateMainMenu()
        {
            var menu = new List<Control>();
            menu.Add(new MenuItem
            {
                Header = "_New Voice-Talent",
                Command = NewVoiceTalentCommand
            });
            menu.Add(new Separator());
            menu.Add(new MenuItem
            {
                Header = "_Delete Voice-Talent",
                Command = DeleteCommand
            });
            menu.Add(new MenuItem
            {
                Header = "Export Voice Library",
                //Command = DeleteProjectCommand
            });
            MainMenu = menu;
        }
        private void SetCharacter(SelectActor msg)
        {
            _selectedCharacter = msg.SelectedCharacter;
            MessengerInstance.Send<OpenSelectActorWindow>(new OpenSelectActorWindow());
        }
        #endregion


    }
}
