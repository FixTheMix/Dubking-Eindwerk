using DubKing.Helpers;
using DubKing.Messages;
using DubKing.Model;
using DubKing.Model.Extentions;
using DubKing.Services.Contracts;
using DubKing.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DubKing.ViewModel.EpisodeView
{
    public enum ColumnTypeSelector { Character, Timecode, Text }
    public class ImportEpisodeViewModel : ViewModelBase, IDataErrorInfo
    {
        #region Fields
        private string _filePath;
        private ICommand _browsCommand;
        private ICommand _SaveEpisodeCommand;
        private string _title = string.Empty;
        private string _translation = string.Empty;
        private string _number=string.Empty;
        private bool _hasCustomCode;
        private string _customCode=string.Empty;
        private Project _project;
        private IEpisodeService _episodeService;
        private ICharacterService _characterService;
        private ILineService _lineService;
        private ObservableCollection<string[]> _file;
        private ColumnTypeSelector _columnOne = ColumnTypeSelector.Character;
        private ColumnTypeSelector _columnTwo = ColumnTypeSelector.Timecode;
        private ColumnTypeSelector _columnTree = ColumnTypeSelector.Text;
        private bool _firstRowHeader;
        private ObservableCollection<int> _availableEpNumbers;
        private List<string> _translators;
        #endregion

        #region Properties
        public string FilePath
        {
            get { return _filePath; }
            set { Set(ref _filePath, value); }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
                TitleValidation();
            }
        }

        private void TitleValidation()
        {
            LengthValidation(150, _title, nameof(Title));
        }

        public string Translation
        {
            get { return _translation; }
            set
            {
                Set(ref _translation, value);
                TranslationValidation();
            }
        }

        private void TranslationValidation()
        {
            LengthValidation(25, _translation, nameof(Translation));
        }

        public string Number
        {
            get { return _number; }
            set
            {
                Set(ref _number, value.RemoveNonDigit());
                NumberValidation();
            }
        }

        private void NumberValidation()
        {
            bool parsed = Int32.TryParse(_number, out int num);
            MandatoryValidation(_number, nameof(Number));
            if (_availableEpNumbers.Count() != 0)
            {
                if (!_availableEpNumbers.Contains(num) && num <= _availableEpNumbers.Max())
                {
                    AddErrorMessage(nameof(Number), "Number Not Available.");
                }
                else
                {
                    RemoveErrorMessage(nameof(Number), "Number Not Available.");
                }
            }
        }

        public bool HasCustomCode
        {
            get { return _hasCustomCode; }
            set
            {
                Set(ref _hasCustomCode, value);
                if(!value) CustomCodeValidation();
            }
        }
        public string CustomCode
        {
            get { return _customCode; }
            set
            {
                Set(ref _customCode, value);
                CustomCodeValidation();
            }
        }
        public ICommand BrowsCommand
        {
            get { return _browsCommand ?? (_browsCommand = new RelayCommand(OnBrowsFile)); }
        }
        public ICommand SaveEpisodeCommand
        {
            get { return _SaveEpisodeCommand ?? (_SaveEpisodeCommand = new RelayCommand(OnSaveEpisode)); }
        }
        public ObservableCollection<string[]> File
        {
            get { return _file; }
            set { _file = value; }
        }
        public ColumnTypeSelector ColumnOne
        {
            get { return _columnOne; }
            set { Set(ref _columnOne, value); }
        }
        public ColumnTypeSelector ColumnTwo
        {
            get { return _columnTwo; }
            set { Set(ref _columnTwo, value); }
        }
        public ColumnTypeSelector ColumnTree
        {
            get { return _columnTree; }
            set { Set(ref _columnTree, value); }
        }
        public bool FirstRowHeader
        {
            get { return _firstRowHeader; }
            set { Set(ref _firstRowHeader, value); }
        }
        public ObservableCollection<int> AvailableEpNumbers
        {
            get
            {
                return _availableEpNumbers;
            }
            set { _availableEpNumbers = value; }
        }

        public List<String> Translators => _translators;
        #endregion
        public bool IsValid
        {
            get
            {
                Validate();
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
        protected void MandatoryValidation(string value, [CallerMemberName]string propertyName = "")
        {
            bool notValid = string.IsNullOrEmpty(value);
            if (notValid)
            {
                AddErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(propertyName, Constants.ErrorMessages.Mandatory);
            }
        }
        protected void LengthValidation(int maxLenght, string value, [CallerMemberName]string propertyName = "")
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
        private void CustomCodeValidation()
        {
            var error = Constants.ErrorMessages.Mandatory;
            if (HasCustomCode)
            {
                MandatoryValidation(_customCode, nameof(CustomCode));
                LengthValidation(10, _customCode, nameof(CustomCode));
            }
            else
            {
                if (_errorMessages.ContainsKey(nameof(CustomCode)))
                {
                    _errorMessages.Remove(nameof(CustomCode));
                }
                RaisePropertyChanged(nameof(CustomCode));
            }
            
        }

        private void Validate()
        {
            CustomCodeValidation();
            TitleValidation();
            NumberValidation();
            TranslationValidation();
        }


        #region Commands
        private void OnBrowsFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV File(.csv)| *.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
            }
            if (!string.IsNullOrEmpty(FilePath))
            {
                ReadCSV();
            }
        }
        private void OnSaveEpisode()
        {
            if (!IsValid) return;
            if (string.IsNullOrEmpty(FilePath))
            {
                ShowError("Please Select a .csv File", "There is no file selected to import.\n Please select a file.");
                return;
            }
            var episode = CreateEpisode();
            if (_firstRowHeader)
            {
                _file.RemoveAt(0);
            }
            var lines = CreateList(episode);
            if (lines != null)
            {
                var ep = SaveEpisode(episode);
                _lineService.SaveLines(lines, episode);
                //ep.Lines = _lineService.Get(ep).ToList();
                //_project.Episodes.Add(ep);
                Messenger.Default.Send(new EpisodeAdded(ep));
            }
            else
            {
                ShowError("Something went wrong!", "Can't create Lines");
            }

            Messenger.Default.Send(new MessageCloseWindow());
            Messenger.Default.Send(new ReloadTableMessage());

            ResetFields();

        }
        #endregion

        #region Constructor
        public ImportEpisodeViewModel(IEpisodeService episodeService, ICharacterService characterService, ILineService lineService)
        {
            Messenger.Default.Register<SetCurrentProject>(this, LoadSelectedProject);
            _characterService = characterService;
            _lineService = lineService;
            _episodeService = episodeService;
            _file = new ObservableCollection<string[]>();
            _availableEpNumbers = new ObservableCollection<int>();
        }
        #endregion

        #region Methodes
        private void ResetFields()
        {
            FilePath = "";
            File.Clear();
            this.Title = "";
            Translation = "";
            Number = "0";
            CustomCode = "";
            FirstRowHeader = false;
            GetAvailableNumbers();
            SetEpNumber();
            ResetValidation();
        }

        private void ResetValidation()
        {
            _errorMessages.Clear();
        }

        private void LoadSelectedProject(SetCurrentProject project)
        {
            _project = project.CurrentProject;
            GetAvailableNumbers();
            SetEpNumber();
            SetTranslators();
        }
        private void SetTranslators()
        {
            _translators = _episodeService.GetAllTranslators().ToList();
            RaisePropertyChanged(nameof(Translators));
        }
        private void GetAvailableNumbers()
        {
            var epNumbers = _project.Episodes.Select(e => e.Number);
            int maxNumb = 0;
            _availableEpNumbers.Clear();
            if (epNumbers.Count() != 0)
            {
                maxNumb = epNumbers.Max();
            }
            for (int i = 1; i < maxNumb + 25; i++)
            {
                if (!epNumbers.Contains(i))
                {
                    _availableEpNumbers.Add(i);
                }
            }
        }
        private void SetEpNumber()
        {
            Number = _availableEpNumbers.Min().ToString();
        }
        private void ReadCSV()
        {
            string line;
            _file.Clear();
            try
            {
                StreamReader file = new StreamReader(_filePath);
            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    File.Add(line.Split(';'));
                }
            }
            catch (Exception ex)
            {
                    throw ex;
            }
            finally
            {
                file.Close();
            }
            }
            catch (IOException exs)
            {
                var dialog = new ErrorMessageDialog("Can't read selected File", $"Unable to read the selected file.\nThe file is either a unvalid .CSV file or is corrupt.\nPlease select an other file.{exs}", "Ok");
                dialog.ShowDialog();
            }
        }
        private Episode SaveEpisode(Episode episode)
        {
            return _episodeService.SaveEpisode(episode);
        }
        private Episode CreateEpisode()
        {
            var episode = new Episode(_project)
            {
                Title = Title,
                Translator = Translation,
                Number = Int32.Parse(Number),
                CustomCodeToggle = HasCustomCode,
                CustomCode = CustomCode,
            };
            return episode;
        }
        private List<Line> CreateList(Episode episode)
        {
            if (!ValidateColumnTypes())
            {
                return null;
            }
            List<Line> list = new List<Line>();
            foreach (string[] line in _file.AsParallel())
            {
                if (CreateTimecode(GetItem(ColumnTypeSelector.Timecode, line), out Timecode timecode))
                {

                    var character = new Character(_project)
                    {
                        Name = GetItem(ColumnTypeSelector.Character, line)
                    };

                    var text = GetItem(ColumnTypeSelector.Text, line);
                    if (timecode == null || character == null || text == null)
                    {
                        return null;
                    }
                    list.Add(new Line(episode)
                    {
                        Character = character,
                        OriginalCharacter = character,
                        OriginalCharacterId = character.CharacterId,
                        Timecode = timecode,
                        OriginalTimecode = timecode,
                        Text = text,
                        OriginalText = text,
                        //Episode = episode
                    });
                }
                else
                {
                    return null;
                }
            }
            return list;
        }
        private string GetItem(ColumnTypeSelector type, string[] line)
        {
            if (line.Length >= 3)
            {
                if (ColumnOne == type)
                {
                    return line[0];
                }
                if (ColumnTwo == type)
                {
                    return line[1];
                }
                return line[2];
            }
            else
            {
                ShowError("Columns must be unique.", "all columns mus have a unique type");
                return null;
            }
        }
        private bool ValidateColumnTypes()
        {
            if (ColumnOne != ColumnTwo && ColumnOne != ColumnTree && ColumnTwo != ColumnTree)
            {
                return true;
            }
            return false;
        }
        private Character GetCharacter(string characterName)
        {
            //UIt het project Halen?
            return _characterService.GetByName(characterName, _project);
        }
        private bool CreateTimecode(string timecodeString, out Timecode tc)
        {
            var numbers = timecodeString.Split(':');
            tc = new Timecode(_project.FrameRate);
            if (numbers.Length == 2)
            {
                string extra = "00";
                string[] newNumbers = new string[4];
                newNumbers[0] = extra;
                newNumbers[1] = numbers[0];
                newNumbers[2] = numbers[1];
                newNumbers[3] = extra;
                numbers = newNumbers;
            }
            if (numbers.Length < 4)
            {
                var dial = new ErrorMessageDialog("Invalid Timecode Format", "Timecode must contain hours, minutes, seconds and frames, seperated by ':'. ", "Ok");
                dial.ShowDialog();
                return false;
            }
            if (Int32.TryParse(numbers[0], out int number))
            {
                tc.Hour = number;
            }
            else
            {
                var dial = new ErrorMessageDialog("Invalid Timecode Format", "TimeCode can only contain numbers seperated by ':'.", "Ok");
                dial.ShowDialog();
                return false;
            }
            if (Int32.TryParse(numbers[1], out number))
            {
                tc.Minute = number;
            }
            else
            {
                var dial = new ErrorMessageDialog("Invalid Timecode Format", "TimeCode can only contain numbers seperated by ':'.", "Ok");
                dial.ShowDialog();
                return false;
            }
            if (Int32.TryParse(numbers[2], out number))
            {
                tc.Second = number;
            }
            else
            {
                var dial = new ErrorMessageDialog("Invalid Timecode Format", "TimeCode can only contain numbers seperated by ':'.", "Ok");
                dial.ShowDialog();
                return false;
            }
            if (Int32.TryParse(numbers[0], out number))
            {
                tc.Hour = number;
            }
            else
            {
                var dial = new ErrorMessageDialog("Invalid Timecode Format", "TimeCode can only contain numbers seperated by ':'.", "Ok");
                dial.ShowDialog();
                return false;
            }
            return true;

        }
        private void ShowError(string title, string message)
        {
            var dial = new ErrorMessageDialog(title, message, "Ok");
            dial.ShowDialog();
        }

        #endregion


    }
}
