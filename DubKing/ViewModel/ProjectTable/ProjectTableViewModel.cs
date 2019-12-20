using DubKing.Helpers;
using DubKing.Messages;
using DubKing.Model;
using DubKing.Model.Extentions;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DubKing.ViewModel.ProjectTable
{
    public class ProjectTableViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields

        private Project _project;
        private readonly IUserService _userService;
        private readonly ICharacterService _characterService;
        private readonly ILineService _lineService;
        private readonly IEpisodeService _episodeService;
        private readonly IProjectTableService _projectTableService;
        private ObservableCollection<TableRowViewModel> _tableRows;
        private TableRow[] _filteredTableRows;
        private ObservableCollection<Episode> _episodes;
        private IEnumerable<Episode> _filteredEpisodes;
        private TableRowViewModel _selectedRow;
        private Character _characterCopy;
        private int _selectedTabIndex;
        private ICommand _excecuteCommand;
        private Character _characterEdit;
        private bool _betweenRange;
        private List<Character> _characters;
        private Episode _from;
        private Episode _to;
        private bool _markAsRec;
        private ICommand _filterRows;
        private ICommand _clearSearch;
        private string _searchField = "";
        private ICommand _setStatus;
        private ICommand _columnFilter;
        private bool _hideRecording;
        private bool _hideMixing;
        private bool _hideMastereing;
        private bool _hideDelivering;
        private SortingOptions _sortBy = SortingOptions.Quantity;
        private DisplayOptions _display = DisplayOptions.Avg;

        #endregion

        #region Properties
        public Project Project { get => _project; }
        public TableRow[] FilteredTableRows
        {
            get => _filteredTableRows == null ? _project.TableRows.Values.ToArray() : _filteredTableRows;
            set => Set(ref _filteredTableRows, value);
        }
        public ObservableCollection<Episode> Episodes
        {
            get => _episodes;
            set
            {
                Set(ref _episodes, value);
            }
        }
        public IEnumerable<Episode> FilteredEpisodes
        {
            get => _filteredEpisodes == null ? _episodes : _filteredEpisodes;
            set
            {
                Set(ref _filteredEpisodes, value);
            }
        }
        public TableRowViewModel SelectedRow
        {
            get => _selectedRow;
            set => Set(ref _selectedRow, value);
        }
        public Character CharacterEdit
        {
            get => _characterEdit;
            set => Set(ref _characterEdit, value);
        }
        public Character CharacterCopy
        {
            get => _characterCopy;
            set => Set(ref _characterCopy, value);
        }
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => Set(ref _selectedTabIndex, value);
        }
        public bool BetweenRange { get => _betweenRange; set => Set(ref _betweenRange, value); }
        public bool MarkAsRecorded { get => _markAsRec; set => Set(ref _markAsRec, value); }
        public Episode From
        {
            get
            {
                if (_from == null)
                {
                    if (Episodes.Count > 0)
                    {
                        return Episodes[0];
                    }
                }
                return _from;
            }
            set
            {
                Set(ref _from, value);
            }
        }
        public Episode To
        {
            get
            {
                if (_to == null)
                {
                    if (Episodes.Count > 0)
                    {
                        return Episodes.Last();
                    }
                }
                return _to;
            }
            set
            {
                Set(ref _to, value);
            }
        }
        public string DuplicateCount { get => _duplicateCount;
            set
            {
                _duplicateCount = value.RemoveNonDigit();
                IsValidDuplicateCount();
            }
        }
        public ICommand Excecute { get => _excecuteCommand ?? (_excecuteCommand = new RelayCommand(OnExcecute)); }
        public ICommand FilterRows { get => _filterRows ?? (_filterRows = new RelayCommand(OnFilterRows)); }
        public ICommand ClearSearch { get => _clearSearch ?? (_clearSearch = new RelayCommand(OnClearSearch)); }
        public ICommand SetStatus { get => _setStatus ?? (_setStatus = new RelayCommand<Episode>(OnSetSatus)); }
        public bool HideRecording { get => _hideRecording; set => Set(ref _hideRecording, value); }
        public bool HideMixing { get => _hideMixing; set => Set(ref _hideMixing, value); }
        public bool HideMastering { get => _hideMastereing; set => Set(ref _hideMastereing, value); }
        public bool HideDelivering { get => _hideDelivering; set => Set(ref _hideDelivering, value); }
        public string SearchField { get => _searchField; set => Set(ref _searchField, value); }
        public DisplayOptions Display
        {
            get => _display; set
            {
                if (value != _display)
                {
                    Set(ref _display, value);
                }
            }
        }
        private ICommand _changeDisplay;
        public string Seperator { get; set; }
        public SortingOptions SortBy
        {
            get { return _sortBy; }
            set
            {
                Set(ref _sortBy, value);
                OnSort();
            }
        }
        private ICommand _openScenarioCommand;
        public ICommand OpenScenario { get => _openScenarioCommand ?? (_openScenarioCommand = new RelayCommand<LineCount>(OnShowScenario)); }
        private ICommand _updateCharacter;
        public ICommand UpdateCharacterCommand { get => _updateCharacter ?? (_updateCharacter = new RelayCommand<Character>(OnUpdateCharacter)); }
        private User _activeUser;
        public IEnumerable<string> CharacterNames { get => _project.Characters.Select(_ => _.Name); }
        public User ActiveUser
        {
            get { return _activeUser; }
        }

        private ICommand _FilterCharactersOnEpisode;
        public ICommand FilterRowsOnEpisode { get => _FilterCharactersOnEpisode ?? (_FilterCharactersOnEpisode = new RelayCommand<Episode>(OnFilterRowsOnEpisode)); }

        private void OnFilterRowsOnEpisode(Episode obj)
        {
            FilteredTableRows = _project.TableRows.Values.Where(_ => obj.Characters.Keys.Select(c=>c.CharacterId).Contains(_.Character.CharacterId)).ToArray();
        }

        private void OnUpdateCharacter(Character c)
        {
            _characterService.Update(c);
        }

        #endregion

        #region Constructor
        public ProjectTableViewModel(Project project, ICharacterService characterService, ILineService lineService, IEpisodeService episodeService, IProjectTableService projectTableService, IUserService userService)
        {
            _project = project;
            _userService = userService;
            _userService.ActiveUserChanged += SetActiveUser;
            SetActiveUser(_userService.GetActiveUser());
            _characterService = characterService;
            _lineService = lineService;
            _episodeService = episodeService;
            _projectTableService = projectTableService;
            // Is net gemaakt in ProjectDetails??? Doorgeven??
            _episodes = new ObservableCollection<Episode>(_project.Episodes);
            LoadTableRows();
            //FilterTableRowEpisodes();
            MessengerInstance.Register<ReloadTableMessage>(this, ReloadTable);
        }
        #endregion



        #region Methodes
        private void SetActiveUser(User user)
        {
            _activeUser = user;
            RaisePropertyChanged(nameof(ActiveUser));
        }
        private void OnShowScenario(LineCount l)
        {
            MessengerInstance.Send<OpenScenarioWindow>(new OpenScenarioWindow(l.Episode, l.Character));
        }
        private void OnSort()
        {
            FilteredTableRows = Sort(FilteredTableRows);
        }
        private void OnClearSearch()
        {
            FilteredTableRows = null;
            SearchField = "";
            OnSort();
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
        public void LoadProjectTable(Project project)
        {
            _project = project;
            Episodes = new ObservableCollection<Episode>(_project.Episodes.OrderBy(e => e.CustomCode).ThenBy(e=>e.Number));
            //TableRows.Clear();
            LoadTableRows();
            RaisePropertyChanged(nameof(FilteredEpisodes));
            //FilterTableRowEpisodes();
        }
        private void RefreshProject()
        {
            _project = _projectTableService.GetProjectTable(_project);
        }

        private void LoadTableRows()
        {
            //TableRows = new ObservableCollection<TableRowViewModel>(_project.TableRows.Select(t => CreateTableRow(t)));
            //OnSort();
            //for (int i = 0; i < _tableRows.Count; i++)
            //{
            //    _tableRows[i].FilterColumns(FilteredEpisodes);
            //}
            OnFilterRows();
        }

        private void OnExcecute()
        {
            switch (_selectedTabIndex)
            {
                case 0:
                    if (!_characterCopy.IsValid) return;
                    SaveRename();
                    break;
                case 1:
                    if (!IsValidDuplicateCount()) return;
                    DuplicateCharacter();
                    break;
                case 2:
                    if (!IsValidSeperator()) return;
                    SplitCharacter();
                    break;
                case 3:
                    if (!_characterCopy.IsValid) return;
                    SaveComment();
                    break;
                default:
                    break;
            }
            //MessengerInstance.Send<RedrawTable>(new RedrawTable());
            if (_selectedTabIndex != 3)
            {
                ReloadTable();
            }
            MessengerInstance.Send<CloseEditCharacterWindow>(new CloseEditCharacterWindow());
        }
        public bool IsValid
        {
            get
            {
                if (_errorMessages.Count == 0)
                {
                    return true;
                }
                return false;

            }
        }
        protected Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }
        protected void AddErrorMessage(string propertyName, string errorMessage)
        {
            if (!_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages[propertyName] = new List<string>();
            }
            if (!_errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Add(errorMessage);
            }
            RaisePropertyChanged(propertyName);
        }
        protected void RemoveErrorMessage(string propertyName, string errorMessage)
        {
            if (_errorMessages.ContainsKey(propertyName) && _errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Remove(errorMessage);
                if (_errorMessages[propertyName].Count == 0)
                {
                    _errorMessages.Remove(propertyName);
                }
            }
            RaisePropertyChanged(propertyName);
        }
        protected bool MandatoryValidation(string propertyName, string value)
        {
            bool notValid = string.IsNullOrEmpty(value);
            if (notValid)
            {
                AddErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
                return false;
            }
            else
            {
                RemoveErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
                return true;
            }
        }
        protected void LengthValidation(string propertyName, int maxLenght, string value)
        {
            string error = Constants.ErrorMessages.LessthenVariable(maxLenght);
            if (value.Length > maxLenght)
            {
                AddErrorMessage(propertyName, error);
            }
            else
            {
                RemoveErrorMessage(propertyName, error);
            }
        }

        private bool IsValidDuplicateCount()
        {
            MandatoryValidation(nameof(DuplicateCount), DuplicateCount);
            if (string.IsNullOrWhiteSpace(_duplicateCount))
            {
                return false;
            }
            var error = Constants.ErrorMessages.MinimumValue(2);
            if (Int32.Parse(DuplicateCount) < 2)
            {
                AddErrorMessage(nameof(DuplicateCount), error);
                return false;
            }
            else
            {
                RemoveErrorMessage(nameof(DuplicateCount), error);
                return true;
            }
        }
        private bool IsValidSeperator()
        {
            return MandatoryValidation(nameof(Seperator), Seperator);
        }
        private void SplitCharacter()
        {
            _lineService.SplitLines(CharacterEdit, Seperator, _project);
        }

        private void DuplicateCharacter()
        {
            _lineService.Duplicate(Int32.Parse(_duplicateCount), CharacterEdit, From, To, BetweenRange);
        }

        private void OnFilterRows()
        {
            if (_project.TableRows != null)
            {
                IEnumerable<TableRow> filter1;
                if (string.IsNullOrWhiteSpace(SearchField))
                {
                    filter1 = _project.TableRows.Values;
                }
                else
                {
                    filter1 = _project.TableRows.Values.AsParallel().Where(t => t.Character.Name.ToUpper().Contains(SearchField.ToUpper()) || t.Character.VoiceTalent.FullName.ToUpper().Contains(SearchField.ToUpper()));
                }
                FilteredTableRows = Sort(filter1);

                View.DubKingProgress.ProgressBarWindow.Close();
            }
        }

        private TableRow[] Sort(IEnumerable<TableRow> filter1)
        {
            IEnumerable<TableRow> sort;
            switch (SortBy)
            {
                case SortingOptions.Character:
                    sort = filter1.AsParallel().OrderBy(t => t.Character.Name).ThenBy(t => t.TotalLineCount);
                    break;
                case SortingOptions.Actor:
                    sort = filter1.AsParallel().OrderBy(t => t.Character.VoiceTalent.FullName).ThenBy(t => t.TotalLineCount);
                    break;
                case SortingOptions.Quantity:
                    //Array.Sort(_project.TableRows.Values, new TableRowQuantityComparer());
                    //sort = _project.TableRows;
                    sort = filter1.AsParallel().OrderByDescending(t => t.TotalLineCount).ThenBy(t => t.Character.Name);
                    break;
                default:
                    sort = filter1;
                    break;
            }

            return sort.ToArray();
        }

        private void SaveRename()
        {
            Character newCharacter = _project.Characters.SingleOrDefault<Character>(c => c.Name == CharacterCopy.Name && c.CharacterId != 0);
            if (newCharacter == null)
            {
                newCharacter = _characterService.Create(CharacterCopy, _project);
            }
            Episode[] episodes = _project.Episodes.ToArray();
            for (int i = 0; i < episodes.Length; i++)
            {
                if (BetweenRange)
                {
                    if (string.Compare(episodes[i].CustomCode, From.CustomCode) >= 0)
                    {
                        if (string.Compare(episodes[i].CustomCode, To.CustomCode) > 0)
                        {
                            break;
                        }
                        _lineService.UpdateAllLines(CharacterEdit, newCharacter, episodes[i], MarkAsRecorded);
                    }
                }
                else
                {
                    _lineService.UpdateAllLines(CharacterEdit, newCharacter, episodes[i], MarkAsRecorded);
                }
            }
            _characterService.RemoveUnused();
        }

        private void ReloadTable(ReloadTableMessage msg = null)
        {
            RefreshProject();
            LoadTableRows();
        }

        private ICommand _openEditCharacter;
        public ICommand OpenEditCharacter { get => _openEditCharacter ?? (_openEditCharacter = new RelayCommand<TableRow>(OpenEditCharacterWindow)); }

        private ICommand _openSelectActor;
        public ICommand OpenSelectActor { get => _openSelectActor ?? (_openSelectActor = new RelayCommand<TableRow>(OpenSelectActorWindow)); }
        private ICommand _selectActor;
        private ICommand _editCharacterCommand;
        private string _duplicateCount = "2";

        public ICommand SelectActor
        {
            get { return _selectActor ?? (_selectActor = new RelayCommand<TableRow>(OpenSelectActorWindow)); }
        }
        public ICommand EditCharacter
        {
            get { return _editCharacterCommand ?? (_editCharacterCommand = new RelayCommand<TableRow>(OpenEditCharacterWindow)); }
        }
        private void SaveComment()
        {
            _characterEdit.Comment = CharacterCopy.Comment;
        }
        private TableRowViewModel CreateTableRow(Character character)
        {
            character.HasChanged += UpdateCharacter;
            var tableRow = new TableRowViewModel(_project.Episodes.ToList(), character);
            //tableRow.DoubleClickCharacterEvent += OpenEditCharacterWindow;
            //tableRow.DoubleClickActorEvent += OpenSelectActorWindow;
            return tableRow;
        }
        private TableRowViewModel CreateTableRow(TableRow t)
        {
            t.Character.HasChanged += UpdateCharacter;
            var tableRow = new TableRowViewModel(t);
            //tableRow.DoubleClickCharacterEvent += OpenEditCharacterWindow;
            //tableRow.DoubleClickActorEvent += OpenSelectActorWindow;
            return tableRow;
        }
        private void UpdateCharacter(object item, EventArgs e)
        {
            _characterService.Update((item as Character));
        }
        private void OpenEditCharacterWindow(TableRow obj)
        {
            CharacterEdit = obj.Character;
            CharacterCopy = new Character(obj.Character);
            MessengerInstance.Send<OpenEditCharacterWindow>(new OpenEditCharacterWindow());
        }
        private void OpenSelectActorWindow(TableRow item)
        {
            MessengerInstance.Send<SelectActor>(new SelectActor(item.Character));
            _characterService.Update(item.Character);
        }
        #endregion
    }
}
