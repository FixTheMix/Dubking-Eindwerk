using DubKing.Messages;
using DubKing.Model;
using DubKing.Services.Contracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DubKing.Model.Extentions;
using System.Windows;
using System.Windows.Controls;

namespace DubKing.ViewModel.Scenario
{
    public class ScenarioViewModel : ViewModelBase
    {
        private readonly ILineService _lineService;
        private readonly ICharacterService _characterService;
        private readonly IEpisodeService _episodeService;
        private readonly IGlossaryService _glossaryService;
        private readonly IUserService _userService;
        private Episode _episode;
        private Character _character;
        private Line _selectedLine;
        private ICommand _selectLine;
        private ICommand _setLineStatus;
        private ICommand _addLine;
        private ICommand _deleteLine;
        private ObservableCollection<Line> _lines;
        private ICommand _BackupSelectedLine;
        private ICommand _saveChanges;
        private ICommand _undoChanges;
        private string _lineCharacterName;
        private string _lineText;
        private string _lineComment;
        private string _lineTimeCode;
        private bool _lineIsRecorded;
        private ICommand _findNext;
        private ICommand _findPrevious;
        private IEnumerable<Line> _filteredLines;
        private ICommand _search;
        private string _characterSearch = string.Empty;
        private string _textSearch = string.Empty;
        private bool _searchEpisode = true;
        private string _offsetTimecode;
        private ICommand _setOriginal;
        private ICommand _offSetPlus;
        private ICommand _offSetMin;
        private ICommand _setOffset;
        private ICommand _calculateOffset;
        private string _destinationTimecode;
        private ICommand _setCalculation;
        private ICommand _close;
        private ICommand _cancelSearch;
        private bool _searchProject;
        private ICommand _firstNotRec;
        private List<Control> _mainMenu;
        private ICommand _logOut;
        private ICommand _exportScenario;
        private ICommand _print;
        private string _newKeyword;
        private string _newKeywordComment;
        private ICommand _openNewKeywordwindow;
        private ICommand _editKeywordComment;
        private ICommand _deleteKeywordComment;


        
        

        public List<Control> MainMenu
        {
            get { return _mainMenu; }
            private set { _mainMenu = value; }
        }
        public Episode Episode
        {
            get { return _episode; }
            set
            {
                Set(ref _episode, value);
                RaisePropertyChanged(nameof(ProjectCharacters));
            }
        }
        public Character Character
        {
            get { return _character; }
            set { Set(ref _character, value); }
        }
        public IEnumerable<Character> ProjectCharacters { get => Episode.Project.Characters; }
        public ObservableCollection<Line> Lines
        {
            get { return _lines; }
            set
            {
                Set(ref _lines, value);
                RaisePropertyChanged(nameof(FilteredLines));
            }
        }
        public IEnumerable<Line> FilteredLines { get => _filteredLines == null ? _lines : _filteredLines; set => Set(ref _filteredLines, value); }
        public Line SelectedLine
        {
            get { return _selectedLine; }
            set
            {
                OnSaveChanges();
                Set(ref _selectedLine, value);
                OnSelectLine();
            }
        }
        public string LineCharacterName { get => _lineCharacterName; set => Set(ref _lineCharacterName, value); }
        public string LineTimeCode { get => _lineTimeCode; set => Set(ref _lineTimeCode, value.RemoveNonDigit().FullTimeCode()); }
        public string LineText { get => _lineText; set => Set(ref _lineText, value); }
        public string LineComment { get => _lineComment; set => Set(ref _lineComment, value); }
        public bool LineIsRecorded { get => _lineIsRecorded; set => Set(ref _lineIsRecorded, value); }
        public string CharacterSearch { get => _characterSearch; set => Set(ref _characterSearch, value); }
        public string TextSearch { get => _textSearch; set => Set(ref _textSearch, value); }
        public bool SearchProject { get => _searchProject; set => Set(ref _searchProject, value); }
        public bool SearchEpisode { get => _searchEpisode; set => _searchEpisode = value; }
        public ICommand SelectLine { get => _selectLine ?? (_selectLine = new RelayCommand<Line>(OnSelectLine)); }
        public ICommand SetLineStatus { get => _setLineStatus ?? (_setLineStatus = new RelayCommand(OnSetLineStatus, OnCanSetLineStatus)); }
        public ICommand AddLine { get => _addLine ?? (_addLine = new RelayCommand(OnAddLine, OnCanAddLine)); }
        public ICommand DeleteLine { get => _deleteLine ?? (_deleteLine = new RelayCommand(OnDeleteLine, OnCanDeleteLine)); }
        public ICommand UndoChanges { get => _undoChanges ?? (_undoChanges = new RelayCommand(OnUndoChangesLine)); }
        public ICommand SaveChanges { get => _saveChanges ?? (_saveChanges = new RelayCommand(OnSaveChanges)); }
        public ICommand BackupSelectedLine { get => _BackupSelectedLine ?? (_BackupSelectedLine = new RelayCommand(OnSelectLine)); }
        public ICommand FindNext { get => _findNext ?? (_findNext = new RelayCommand(OnFindNext, OnCanFind)); }
        public ICommand OffSetPlus { get => _offSetPlus ?? (_offSetPlus = new RelayCommand<TextBox>(OnOffsetPlus, OnCanOffsetPlus)); }

        private bool OnCanOffsetPlus(TextBox arg)
        {
            return _activeUser.ProjectAccess != ProjectModuleAccess.ReadOnly;
        }

        public ICommand OffSetMin { get => _offSetMin ?? (_offSetMin = new RelayCommand<TextBox>(OnOffsetMin, OnCanOffsetPlus)); }
        public string OffsetTimeCode
        {
            get { return _offsetTimecode; }
            set { Set(ref _offsetTimecode, value.FullOffset()); }
        }
        public ICommand SetOffset { get => _setOffset ?? (_setOffset = new RelayCommand(OnSetOffset, OnCanSetOffset)); }

        private bool OnCanSetOffset()
        {
            return _activeUser.ProjectAccess != ProjectModuleAccess.ReadOnly;
        }

        public ICommand FindPrevious { get => _findPrevious ?? (_findPrevious = new RelayCommand(OnFindPrevious, OnCanFind)); }
        public ICommand Search { get => _search ?? (_search = new RelayCommand(OnSearch)); }
        public ICommand SetOriginal { get => _setOriginal ?? (_setOriginal = new RelayCommand(OnSetOriginal, OnCanSetOriginal)); }
        public ICommand CalculateOffset { get => _calculateOffset ?? (_calculateOffset = new RelayCommand(OnCalculateOffset, OnCanSetOffset)); }
        public string DestinationTimecode { get => _destinationTimecode; set => _destinationTimecode = value.FullTimeCode(); }
        public ICommand SetCalculation { get => _setCalculation ?? (_setCalculation = new RelayCommand(OnSetCalculation, OnCanSetOffset)); }
        public ICommand Close { get => _close ?? (_close = new RelayCommand(OnClose)); }
        public ICommand CancelSearch { get => _cancelSearch ?? (_cancelSearch = new RelayCommand(OnCancelSearch)); }
        public ICommand FirstNotRec { get => _firstNotRec ?? (_firstNotRec = new RelayCommand(OnFirstNotRec, OnCanFirstNotRec)); }
        public ICommand ExportScenario { get => _exportScenario ?? (_exportScenario = new RelayCommand(OnExportScenario)); }
        public ICommand Print { get => _print ?? (_print = new RelayCommand(OnPrint)); } 
        public ICommand LogOut { get => _logOut ?? (_logOut = new RelayCommand(OnLogout)); }
        public string NewKeyword
        {
            get { return _newKeyword; }
            set { Set(ref _newKeyword, value); }
        }
        public string NewKeywordComment 
        {
            get { return _newKeywordComment; }
            set { Set(ref _newKeywordComment, value); }
        }
        public ICommand OpenNewKeywordWindow { get => _openNewKeywordwindow ?? (_openNewKeywordwindow = new RelayCommand<string>(OnOpenNewKeywordWindow, OnCanOpenKeyword)); }

        private bool OnCanOpenKeyword(string arg)
        {
            return _activeUser.ProjectAccess != ProjectModuleAccess.ReadOnly;
        }

        public IEnumerable<KeywordComment> KeywordComments
        {
            get
            {
                if (SelectedLine == null)
                {
                    return null;
                }
                return _episode.Project.KeyWordComments.Values.Where(_ => SelectedLine.Text.ToUpper().Contains(_.Keyword.ToUpper()));
            }
        }
        public ICommand EditKeywordComment { get => _editKeywordComment ?? (_editKeywordComment = new RelayCommand<KeywordComment>(OnEditKeywordComment, OnCanEditKeyword)); }

        private bool OnCanEditKeyword(KeywordComment arg)
        {
            return _activeUser.ProjectAccess != ProjectModuleAccess.ReadOnly;
        }

        public ICommand DeleteKeywordComment { get => _deleteKeywordComment ?? (_deleteKeywordComment = new RelayCommand<KeywordComment>(OnDeleteKeyworComment, OnCanEditKeyword)); }

        private User _activeUser;

        public User ActiveUser
        {
            get { return _activeUser; }
        }



        public ScenarioViewModel(ILineService lineService, ICharacterService characterService, IEpisodeService episodeService, IGlossaryService glossaryService, IUserService userservice)
        {
            _lineService = lineService;
            _characterService = characterService;
            _episodeService = episodeService;
            _glossaryService = glossaryService;
            _userService = userservice;
            _userService.ActiveUserChanged += SetActiveUser;
            SetActiveUser(_userService.GetActiveUser());
            CreateMainMenu();
            MessengerInstance.Register<LoadScenarioMessage>(this, loadScenario);
        }

        private void SetActiveUser(User user)
        {
            _activeUser = user;
            RaisePropertyChanged(nameof(ActiveUser));
        }

        private void OnEditKeywordComment(KeywordComment obj)
        {
            var msg = SendOpenNewKeywordMessage(false, obj.Keyword, obj.Comment);
            if (!msg.IsSaved || !msg.IsValid) return;
            string keyBU = obj.Keyword;
            string commentBU = obj.Comment;
            obj.Keyword = msg.Keyword;
            obj.Comment = msg.Comment;
            if (!_glossaryService.Update(obj))
            {
                obj.Keyword = keyBU;
                obj.Comment = commentBU;
            }
            
        }
        private void OnDeleteKeyworComment(KeywordComment obj)
        {
            var msg = new ConfirmGlossaryKeywordDeleteMessage(obj);
            MessengerInstance.Send(msg);
            if (msg.CanDelete)
            {
                if (_glossaryService.Delete(obj))
                {
                    _episode.Project.DeleteKeywordComment(obj);
                }
            }
            RaisePropertyChanged(nameof(KeywordComments));
        }
        private OpenNewKeywordMessage SendOpenNewKeywordMessage(bool isNew, string key = "", string comment = "")
        {
            var msg = new OpenNewKeywordMessage(_episode.Project.KeyWordComments.Keys.ToArray(), isNew, key, comment);
            MessengerInstance.Send<OpenNewKeywordMessage>(msg);
            return msg;
        }
        private void OnOpenNewKeywordWindow(string obj)
        {
            var msg = SendOpenNewKeywordMessage(true, obj);
            if (!msg.IsSaved || !msg.IsValid) return;
            _glossaryService.Create(msg.Keyword, msg.Comment, _episode.Project);
            RaisePropertyChanged(nameof(KeywordComments));
        }
        private void OnLogout()
        {
            MessengerInstance.Send(new LogOutMessage());
            OnClose();
        }
        private void OnExportScenario()
        {
            throw new NotImplementedException();
        }
        private void OnPrint()
        {
            throw new NotImplementedException();
        }
        private void OnFirstNotRec()
        {
            var result = _lines.Where(_ => _.Character == _character && !_lineIsRecorded).FirstOrDefault();
            if (result != null)
            {
                SelectedLine = result;
            }
            else
            {
                MessageBox.Show("All lines for this character are recorded.");
            }
        }
        private bool OnCanFirstNotRec()
        {
            return _character != null;
        }
        private void CreateMainMenu()
        {
            var menu = new List<Control>();

            menu.Add(new MenuItem
            {
                Header = "Go to _firt not recorded line",
                Command = FirstNotRec
            });
            menu.Add(new Separator());
            menu.Add(new MenuItem
            {
                Header = "_Print",
                Command = Print,
                IsEnabled = false
            });
            menu.Add(new MenuItem
            {
                Header = "E_xport Scenario",
                Command = ExportScenario,
                IsEnabled = false
            });
            menu.Add(new Separator());
            menu.Add(new MenuItem
            {
                Header = "_Close",
                Command = Close
            });

            MainMenu = menu;
        }
        private void OnCancelSearch()
        {
            SearchProject = false;
            SearchEpisode = true;
            CharacterSearch = string.Empty;
            TextSearch = string.Empty;
            OffsetTimeCode = Episode.Offset.ToString();
            FilteredLines = Lines;

            if (_character != null)
            {
                SelectedLine = Lines.FirstOrDefault<Line>(l => l.Character.CharacterId == Character.CharacterId);
            }
            else
            {
                SelectedLine = Lines.OrderBy(l => l.Timecode.TotalFrames).First<Line>();
            }
        }
        private void OnSetCalculation()
        {
            var current = SelectedLine.Timecode;
            var destination = DestinationTimecode.ParseTimeCode(_episode.Project.FrameRate);
            int offsetFrames = destination.TotalFrames - current.TotalFrames;
            var offset = new TimecodeOffset(_episode.Project.FrameRate);
            offset.SetTotalFrames(offsetFrames);
            OffsetTimeCode = offset.ToString();
            OnSetOffset();
            MessengerInstance.Send<CloseCalculateOffsetWindow>(new CloseCalculateOffsetWindow());
        }
        private void OnClose()
        {
            MessengerInstance.Send(new CloseScenarioMessage());
        }
        private void OnCalculateOffset()
        {
            DestinationTimecode = LineTimeCode;
            MessengerInstance.Send<OpenCalculateOffsetWindow>(new OpenCalculateOffsetWindow());
        }
        private bool OnCanSetOriginal()
        {
            if ( _activeUser.ProjectAccess == ProjectModuleAccess.ReadOnly)
            {
                return false;
            }
            if (_selectedLine == null)
            {
                return false;
            }
            if (_selectedLine.OriginalText != _selectedLine.Text)
            {
                return true;
            }
            if (_selectedLine.OriginalCharacterId != _selectedLine.CharacterId)
            {
                return true;
            }
            if (_selectedLine.Timecode.TotalFrames != _selectedLine.OriginalTimecode.TotalFrames)
            {
                return true;
            }
            return false;
        }
        private void OnSetOriginal()
        {
            SelectedLine.ResetOriginal();
            OnSelectLine();
        }
        private void OnOffsetPlus(TextBox textBox)
        {
            OffsetTimeCode = "+" + OffsetTimeCode;
            textBox.Select(textBox.Text.Length, 0);
        }
        private void OnOffsetMin(TextBox textBox)
        {
            OffsetTimeCode = "-" + OffsetTimeCode;
            textBox.Select(textBox.Text.Length, 0);
        }
        private void OnSetOffset()
        {
            _episode.Offset = OffsetTimeCode.ParseToOffset(_episode.Project.FrameRate);
            OnSelectLine();
            _episodeService.UpdateEpisode(_episode);
        }
        private bool OnCanFind()
        {
            return _selectedLine != null;
        }
        private void OnSearch()
        {
            FilteredLines = null;
            if (SearchProject)
            {
                var lines = new List<Line>();
                foreach (var episode in _episode.Project.Episodes)
                {
                    foreach (Line line in episode.Lines)
                    {
                        lines.Add(line);
                    }
                }
                FilteredLines = lines;
            }

            if (CharacterSearch != "")
            {
                FilteredLines = FilteredLines.Where(l => l.Character.Name.ToUpper().Contains(CharacterSearch.ToUpper()));
            }
            if (TextSearch != "")
            {
                FilteredLines = FilteredLines.Where(l => l.Text.ToUpper().Contains(TextSearch.ToUpper()));
            }
        }
        private Character CheckCharacterExists(Character character)
        {
            Character result = Episode.Project.Characters.SingleOrDefault<Character>(c => c.Name == Character.Name);
            if (result == null)
            {
                return _characterService.Create(result, Episode.Project);
            }
            return result;
        }
        private void OnSelectLine()
        {
            if (SelectedLine != null)
            {
                SetSelectedLineBU();
                RaisePropertyChanged(nameof(KeywordComments));
            }
        }
        private void SetSelectedLineBU()
        {
            LineCharacterName = _selectedLine.Character.Name;
            LineText = _selectedLine.Text;
            LineComment = _selectedLine.Comment;
            LineTimeCode = _selectedLine.TimecodeWithOffset.ToString();
            LineIsRecorded = _selectedLine.IsRecorded;
        }
        private void OnUndoChangesLine()
        {
            OnSelectLine();
        }
        private bool SelectedLineHasChanged()
        {
            if (_selectedLine.Character.Name != _lineCharacterName)
            {
                return true;
            }
            if (_selectedLine.Text != _lineText)
            {
                return true;
            }
            if (_selectedLine.Comment != _lineComment)
            {
                return true;
            }
            if (_selectedLine.TimecodeWithOffset.ToString() != _lineTimeCode)
            {
                return true;
            }
            if (_selectedLine.IsRecorded != _lineIsRecorded)
            {
                return true;
            }
            return false;
        }
        private void OnSaveChanges()
        {
            if (SelectedLine == null)
            {
                return;
            }
            if (!SelectedLineHasChanged()) return;
            var character = Episode.Project.Characters.SingleOrDefault<Character>(c => c.Name == LineCharacterName);
            if (character == null)
            {
                character = _characterService.Create(new Character { Name = LineCharacterName }, Episode.Project);
            }
            SelectedLine.Character = character;


            var tcValues = _lineTimeCode.Split(':');
            SelectedLine.TimecodeWithOffset = new Timecode(Episode.Project.FrameRate)
            {
                Hour = Int32.Parse(tcValues[0]),
                Minute = Int32.Parse(tcValues[1]),
                Second = Int32.Parse(tcValues[2]),
                Frame = Int32.Parse(tcValues[3]),
            };

            SelectedLine.Text = LineText;
            SelectedLine.Comment = LineComment;

            _lineService.UpdateLine(_selectedLine);
            RefreshLines();
        }
        private void OnSetLineStatus()
        {
            SelectedLine.IsRecorded = !SelectedLine.IsRecorded;
            LineIsRecorded = SelectedLine.IsRecorded;
            UpdateLine(_selectedLine);
            Character selectedCharacter = GetSearchCharacter();

            SearchNext(selectedCharacter);

        }
        private Character GetSearchCharacter()
        {
            var selectedCharacter = _selectedLine?.Character;
            if (_character != null)
            {
                selectedCharacter = _character;
            }

            return selectedCharacter;
        }
        private bool OnCanSetLineStatus()
        {
            if (_activeUser.ProjectAccess == ProjectModuleAccess.ReadOnly)
            {
                return false;
            }
            if (_selectedLine == null)
            {
                return false;
            }
            if (Lines.Count() != FilteredLines.Count() && _character != null)
            {
                return false;
            }
            return true;
        }
        private void OnAddLine()
        {
            if (_selectedLine == null)
            {
                return;
            }
            var newLine = new Line(_episode, _selectedLine.Character, _selectedLine.Timecode);
            newLine = _lineService.CreateLine(newLine);
            RefreshLines();
            SelectedLine = newLine;
        }
        private void RefreshLines()
        {
            Lines = new ObservableCollection<Line>(Episode.Lines.OrderBy(l => l.Timecode.TotalFrames));
        }
        private bool OnCanAddLine()
        {
            if (_activeUser.ProjectAccess == ProjectModuleAccess.ReadOnly)
            {
                return false;
            }
            return SelectedLineNotNull();
        }
        private void OnDeleteLine()
        {
            var confirm = new AskConfirmationDeleteLine();
            MessengerInstance.Send<AskConfirmationDeleteLine>(confirm);
            if (confirm.IsConfirmed)
            {
                _lineService.Delete(_selectedLine);
                Episode.RemoveLine(_selectedLine);
                var line = _selectedLine;
                RefreshLines();
                if (!SearchNext(GetSearchCharacter(), false))
                {
                    SelectedLine = FilteredLines.Where(_ => _.Timecode.TotalFrames > line.Timecode.TotalFrames).FirstOrDefault();
                } 
                RaisePropertyChanged(nameof(FilteredLines));
            }

        }
        private bool OnCanDeleteLine()
        {
            if (_activeUser.ProjectAccess == ProjectModuleAccess.ReadOnly)
            {
                return false;
            }
            return SelectedLineNotNull();
        }
        private bool SelectedLineNotNull()
        {
            if (_selectedLine == null)
            {
                return false;
            }
            return true;
        }
        private bool SearchNext(Character selectedCharacter, bool showMsg = true)
        {
            bool takeNext = false;
            var filteredLines = FilteredLines.ToList();
            for (int i = 0; i < filteredLines.Count; i++)
            {
                if (_selectedLine == filteredLines[i])
                {
                    takeNext = true;
                    continue;
                }
                if (takeNext)
                {
                    if (filteredLines[i].Character.CharacterId == selectedCharacter.CharacterId)
                    {
                        SelectedLine = filteredLines[i];
                        return true;
                    }
                }
            }
            if(showMsg) MessageBox.Show("Last Line Selected");
            return false;
        }
        private void OnFindPrevious()
        {
            var selectedCharacter = GetSearchCharacter();
            bool takeNext = false;
            var filteredLines = FilteredLines.ToList();
            for (int i = (filteredLines.Count - 1); i >= 0; i--)
            {
                if (_selectedLine == filteredLines[i])
                {
                    takeNext = true;
                    continue;
                }
                if (takeNext)
                {
                    if (filteredLines[i].Character.CharacterId == selectedCharacter.CharacterId)
                    {
                        SelectedLine = filteredLines[i];
                        return;
                    }
                }
            }
            MessageBox.Show("First Line Selected");
        }
        private void UpdateLine(Line line)
        {
            _lineService.UpdateLine(line);
        }
        private void OnSelectLine(Line obj)
        {
            SelectedLine = obj;
            RaisePropertyChanged(nameof(KeywordComments));
        }
        private void OnFindNext()
        {
            SearchNext(GetSearchCharacter());
        }
        private void loadScenario(LoadScenarioMessage obj)
        {
            Episode = obj.Episode;
            _glossaryService.Get(_episode.Project);
            RaisePropertyChanged(nameof(KeywordComments));
            OffsetTimeCode = Episode.Offset.ToString();
            FilteredLines = null;
            SelectedLine = null;
            Lines = new ObservableCollection<Line>(obj.Episode.Lines.OrderBy(_=>_.Timecode.TotalFrames).ThenBy(_ => _.Character.Name));
            Character = obj.Character;
            if (obj.Character != null)
            {
                SelectedLine = Lines.FirstOrDefault<Line>(l => l.Character.CharacterId == Character.CharacterId);
            }
            else
            {
                SelectedLine = Lines.First<Line>();
            }
            RaisePropertyChanged(nameof(FilteredLines));
        }
    }
}
