using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DubKing.Model;
using DubKing.Model.Contracts;
using DubKing.Model.Extentions;

namespace DubKing.Model
{

    public class Project : INotifyChange, IDataErrorInfo, INotifyDataErrorInfo, INotifyPropertyChanged
    {
        #region Fields
        string _customer;
        string _title;
        string _dubbedLanguage;
        string _originalLanguage;
        private string _comment;
        private ProjectType _projectType;
        private FrameRate _frameRate;
        private TimeSpan _avgDuration;
        private List<User> _autherizedUsers;
        private Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
        private bool _isUnique = true;
        private Episode[] _episodes = new Episode[0];
        private Dictionary<Character, TableRow> _tableRows = new Dictionary<Character, TableRow>();
        private Character[] _characters = new Character[0];
        private Dictionary<string, KeywordComment> _keywordComments = new Dictionary<string, KeywordComment>();

        public event EventHandler HasChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public int ProjectId
        {
            get; set;
        }
        public string Customer
        {
            get { return _customer; }
            set
            {
                _customer = value.ToTitleCase();
                IsUnique = true;
                ValidateCustomer();
                NotifyChange();
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value.ToTitleCase();
                IsUnique = true;
                ValidateTitle();
                NotifyChange();
            }
        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                ValidateComment();
                NotifyChange();
            }
        }
        public ProjectType ProjectType
        {
            get { return _projectType; }
            set
            {
                _projectType = value;
                NotifyChange();
            }
        }
        public string OriginalLanguage
        {
            get { return _originalLanguage; }
            set
            {
                _originalLanguage = value.Trim();
                if (string.IsNullOrEmpty(_originalLanguage))
                {
                    OriginalLanguageId = 0;
                }
                NotifyChange();
            }
        }
        public string DubbedLanguage
        {
            get { return _dubbedLanguage; }
            set
            {
                _dubbedLanguage = value.Trim();
                if (string.IsNullOrEmpty(_dubbedLanguage))
                {
                    DubbedLanguageId = 0;
                }
                NotifyChange();
            }
        }
        public FrameRate FrameRate
        {
            get { return _frameRate; }
            set
            {
                _frameRate = value;
                NotifyChange();
            }
        }
        public TimeSpan AvgDuration
        {
            get { return _avgDuration; }
            set
            {
                _avgDuration = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public List<User> AutherizedUsers
        {
            get { return _autherizedUsers; }
            set
            {
                _autherizedUsers = value;
                NotifyChange();
            }
        }
        public List<Session> Sessions { get; set; }
        public List<SessionLog> SessionLogs { get; set; }
        public Dictionary<string, KeywordComment> KeyWordComments { get => _keywordComments;}
        public int OriginalLanguageId { get; set; }
        public int DubbedLanguageId { get; set; }
        public Dictionary<int, Dictionary<int, LineCount>> LineCounts { get; set; }
        public Episode[] Episodes
        {
            get { return _episodes; }
            set
            {
                _episodes = value;
            }
        }
        public Character[] Characters
        {
            get => _characters;
        }
        public Dictionary<Character, TableRow> TableRows { get => _tableRows; }
        public bool IsValid
        {
            get
            {
                Validate();
                return _errorMessages.Count == 0;
            }
        }
        public bool IsUnique
        {
            get => _isUnique;
            set
            {
                _isUnique = value;
            }
        }
        public bool CanSave
        {
            get
            {
                if (_customer != string.Empty && _title != string.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string Error => throw new NotImplementedException();
        public bool HasErrors => _errorMessages.Count == 0;
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }
        public void ClearChildren()
        {
            _characters = new Character[0];
            _episodes = new Episode[0];
            //_tableRowDictionary = new Dictionary<Character, Dictionary<Episode, LineCount>>();
            _tableRows = new Dictionary<Character, TableRow>();
        }
        public bool HasKeywordComment(string value)
        {
            if (_keywordComments.ContainsKey(value))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Constructors
        public Project()
        {
            _customer = String.Empty;
            _title = string.Empty;
            _comment = String.Empty;
            _projectType = ProjectType.AnimationSeries;
            _originalLanguage = String.Empty;
            _dubbedLanguage = String.Empty;
            _frameRate = FrameRate.fps25;
            _avgDuration = new TimeSpan();
            _autherizedUsers = new List<User>();
            Sessions = new List<Session>();
            SessionLogs = new List<SessionLog>();
            //eyWordComments = new List<KeywordComment>();
        }
        public Project(int id) : this()
        {
            ProjectId = id;
        }
        public Project(int id, string c, string t) : this(id)
        {
            Customer = c;
            Title = t;
        }
        /// <summary>
        /// Initialize new project
        /// </summary>
        /// <param name="c">Project Customer</param>
        /// <param name="t">Project Title</param>
        /// <param name="u">Autherized User</param>
        public Project(int id, string c, string t, User u) : this(id, c, t)
        {
            AutherizedUsers.Add(u);
        }
        #endregion

        #region Methodes
        public void AddAuthorizedUsers(User[] users)
        {
            _autherizedUsers.AddRange(users);
            NotifyChange();
        }
        public void AddKeywordComment(KeywordComment keyword)
        {
            if (_keywordComments.ContainsKey(keyword.Keyword)) return;
            if (!keyword.IsValid) return;
            keyword.Project = this;
            _keywordComments.Add(keyword.Keyword, keyword);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KeyWordComments)));
        }
        public void DeleteKeywordComment(KeywordComment keyword)
        {
            _keywordComments.Remove(keyword.Keyword);
        }
        private void AddErrorMessage(string propertyName, string errorMessage)
        {
            if (!_errorMessages.ContainsKey(propertyName))
            {
                _errorMessages[propertyName] = new List<string>();
            }
            if (!_errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Add(errorMessage);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void RemoveErrorMessage(string propertyName, string errorMessage)
        {
            if (_errorMessages.ContainsKey(propertyName) && _errorMessages[propertyName].Contains(errorMessage))
            {
                _errorMessages[propertyName].Remove(errorMessage);
                if (_errorMessages[propertyName].Count == 0)
                {
                    _errorMessages.Remove(propertyName);
                }
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AddEpisode(Episode ep)
        {
            if (ep == null) return;
            if (ep.Project != this) ep.Project = this;
            if (!_episodes.Select(_=>_.EpisodeId).Contains(ep.EpisodeId))
            {
                var existingEpisodes = _episodes.ToList();
                existingEpisodes.Add(ep);
                _episodes = existingEpisodes.OrderBy(_ => _.EpisodeId).ToArray();
            }
            UpdateTableRowEpisodes(ep);
        }
        private void UpdateTableRowEpisodes(Episode ep)
        {
            foreach (var tr in _tableRows.Values)
            {
                tr.AddEpisode(ep);
            }
        }
        public void AddEpisodes(IEnumerable<Episode> eps)
        {
            var items = eps.ToArray();
            ProcessArray<Episode>(items, AddEpisode);
            
        }
        private void AddEpisodeToTableRows(Episode ep)
        {
            foreach (var tr in _tableRows.Values)
            {
                tr.AddEpisode(ep);
            }
        }
        public void RemoveEpisode(Episode ep)
        {
            var existing = _episodes.ToList();
            existing.Remove(ep);
            foreach (var item in TableRows)
            {
                item.Value.RemoveEpisode(ep);
            }
            RaisePropertyChanged(nameof(Episodes));
        }
        private void RaisePropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public void AddCharacter(Character character)
        {
            if (character == null) return;
            if (character.Project != this) character.Project = this;
            if (!_characters.Select(_=>_.CharacterId).Contains(character.CharacterId))
            {
                var existing = _characters.ToList();
                existing.Add(character);
                if (existing.Count > 1)
                {
                    _characters = existing.OrderBy(_ => _.CharacterId).ToArray();
                }
                else
                {
                    _characters = existing.ToArray();
                }
            }
            //AddLinecounts(character);
            AddTableRow(character);
        }
        public void RemoveCharacter(Character character)
        {
            if (character == null) return;
            var existing = _characters.ToList();
            existing.Remove(character);
            _characters = existing.ToArray();
        }
        private void AddTableRow(Character character)
        {
            if (!_tableRows.ContainsKey(character))
            {
                var tr = new TableRow(character);
                tr.AddEpsiodes(_episodes);
                _tableRows.Add(character, tr);
            }
        }
        private void ProcessArray<T>(T[] items, Action<T> processAction)
        {
            for (int i = 0; i < items.Length; i++)
            {
                processAction(items[i]);
            }
        }
        private void Validate()
        {
            ValidateCustomer();
            ValidateTitle();
            ValidateComment();
        }
        private void ValidateCustomer()
        {
            if (Customer.Length > 50)
            {
                AddErrorMessage(nameof(Customer), Constants.ErrorMessages.LessThen50);
            }
            else
            {
                RemoveErrorMessage(nameof(Customer), Constants.ErrorMessages.LessThen50);
            }
            if (String.IsNullOrEmpty(Customer))
            {
                AddErrorMessage(nameof(Customer), Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(nameof(Customer), Constants.ErrorMessages.Mandatory);
            }
            ValidateUnique();
        }
        private void ValidateComment()
        {
            if (Comment.Length > 1000)
            {
                AddErrorMessage(nameof(Comment), Constants.ErrorMessages.LessThen1k);
            }
            else
            {
                RemoveErrorMessage(nameof(Comment), Constants.ErrorMessages.LessThen1k);
            }
        }
        private void ValidateTitle()
        {
            if (Title.Length > 50)
            {
                AddErrorMessage(nameof(Title), Constants.ErrorMessages.LessThen50);
            }
            else
            {
                RemoveErrorMessage(nameof(Title), Constants.ErrorMessages.LessThen50);
            }
            if (String.IsNullOrEmpty(Title))
            {
                AddErrorMessage(nameof(Title), Constants.ErrorMessages.Mandatory);
            }
            else
            {
                RemoveErrorMessage(nameof(Title), Constants.ErrorMessages.Mandatory);
            }
            ValidateUnique();
        }
        private void ValidateUnique()
        {
            if (!IsUnique)
            {
                AddErrorMessage(nameof(Title), Constants.ErrorMessages.Exists);
            }
            else
            {
                RemoveErrorMessage(nameof(Title), Constants.ErrorMessages.Exists);
            }
        }
        public override string ToString()
        {
            string output = Customer + "-" + Title + "\n";
            int lenght = output.Length;
            output += Comment + "\n";
            output += OriginalLanguage + " - " + DubbedLanguage + "\n";
            output += ProjectType.ToString() + " - " + FrameRate.ToString() + "-" + AvgDuration.ToString() + "\n";
            for (int i = 0; i < lenght; i++)
            {
                output += "*";
            }
            output += "\n\n";
            return output;
        }
        private void NotifyChange()
        {
            HasChanged?.Invoke(this, EventArgs.Empty);
        }
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errorMessages.ContainsKey(propertyName))
            {
                return _errorMessages[propertyName];
            }
            return null;
        }
        #endregion
    }
}
