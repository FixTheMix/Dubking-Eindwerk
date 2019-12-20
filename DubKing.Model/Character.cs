using DubKing.Model.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class Character : INotifyChange, INotifyPropertyChanged, IDataErrorInfo
    {
     
        private string _name;
        private string _comment;
        private bool _status = true;
        private VoiceTalent _voiceTalent = new VoiceTalent();
        private Line[] _lines = new Line[0];
        private Episode[] _episodes = new Episode[0];
        private Project _project;
        private bool _isRecorded;


        protected Dictionary<string, List<string>> _errorMessages = new Dictionary<string, List<string>>();
  
        public int CharacterId { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                ValidateName();
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
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged();
                NotifyChange();
            }
        }
        public Episode[] Episodes { get => _episodes; }
        public Line[] Lines { get => _lines; set => AddLines(value); }
        public VoiceTalent VoiceTalent
        {
            get { return _voiceTalent; }
            set
            {
                SetVoiceTalent(value);
                NotifyChange();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VoiceTalent)));
            }
        }
        public Project Project { get => _project; set => SetProject(value); }
        public string Error => throw new NotImplementedException();
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
        public bool IsRecorded
        {
            get { return _isRecorded; }
        }


        private void Validate()
        {
            ValidateName(nameof(Name));
            ValidateComment(nameof(Comment));
        }
        public string this[string columnName]
        {
            get
            {
                return (!_errorMessages.ContainsKey(columnName) ? null :
                    String.Join(Environment.NewLine, _errorMessages[columnName]));
            }
        }
        private void SetProject(Project p)
        {
            if (p == null) return;
            if (_project != null && p != _project)
            {
                _project.RemoveCharacter(this);
            }
            _project = p;
            _project.AddCharacter(this);
        }
        public void AddLine(Line line)
        {
            if (!_lines.Contains(line))
            {
                var existing = _lines.ToList();
                existing.Add(line);
                _lines = existing.ToArray();
                SetIsRecorded(line.IsRecorded);
                line.IsRecordedChanged += SetIsRecorded;
            }
            AddEpisode(line.Episode);
            if (_project != null) _project.AddCharacter(this);
        }
        public void AddLines(IEnumerable<Line> lines)
        {
            var items = lines.ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                AddLine(items[i]);
            }
        }
        public void RemoveLine(Line line)
        {
            var existingLines = _lines.ToList();
            existingLines.Remove(line);
            _lines = existingLines.ToArray();
            line.IsRecordedChanged -= SetIsRecorded;
            SetIsRecorded();
            if (!_lines.Select(_ => _.Episode).Contains(line.Episode))
            {
                RemoveEpisode(line.Episode);
            }
            if (_episodes.Length == 0 && _project != null)
            {
                _project.RemoveCharacter(this);
            }
        }

        private void SetIsRecorded(object sender = null, bool isRecStatus = true)
        {
            if (isRecStatus == false)
            {
                _isRecorded = false;
            }
            else
            {
                _isRecorded = _lines.Where(_ => _.IsRecorded == false).Count() == 0;
            }
            RaiseIsRecordedChanged();
        }
        public void AddEpisode(Episode episode)
        {
            if (episode == null) return;
            if (_project == null && episode.Project != null)
            {
                _project = episode.Project;
            }
            if (!_episodes.Contains(episode))
            {
                var existing = _episodes.ToList();
                existing.Add(episode);
                if (existing.Count > 1)
                {
                    _episodes = existing.OrderBy(_ => _.EpisodeId).ToArray();
                }
                else
                {
                    _episodes = existing.ToArray();
                }
            }
        }
        private void RemoveEpisode(Episode ep)
        {
            if (ep == null) return;
            var existingEpisodes = _episodes.ToList();
            existingEpisodes.Remove(ep);
            _episodes = existingEpisodes.OrderBy(_ => _.EpisodeId).ToArray();
            ep.RemoveCharacter(this);
            //if(_project != null)_project.UpdateLineCounts(this);
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
            }
            RaisePropertyChanged(propertyName);
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
            }
            RaisePropertyChanged(propertyName);
        }
        private void MandatoryValidation(string propertyName, string value)
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
        private void LengthValidation(string propertyName, int maxLenght, string value)
        {
            string error = Constants.ErrorMessages.LessthenVariable(maxLenght);
            if (value != null)
            {
                if (value.Length > maxLenght)
                {
                    AddErrorMessage(propertyName, error);
                }
            }
            else
            {
                RemoveErrorMessage(propertyName, error);
            }
        }
        public void ValidateUnique(Character[] characters)
        {
            string error = Constants.ErrorMessages.Exists;
            if (characters.Select(_=>_.Name).Where(_ => _ == _name).Count() > 0)
            {
                AddErrorMessage(nameof(Name), error);
            }
            else
            {
                RemoveErrorMessage(nameof(Name), error);
            }
        }
        private void ValidateName([CallerMemberName]string prop = "")
        {
            MandatoryValidation(prop, _name);
            LengthValidation(prop, 50, _name);
        }
        private void ValidateComment([CallerMemberName]string prop = "")
        {
            LengthValidation(prop, 600, _comment);
        }
        private void SetVoiceTalent(VoiceTalent value)
        {
            if (_voiceTalent != value)
            {
                var temp = _voiceTalent;
                _voiceTalent = value;
                if (temp != null)
                {
                    temp.RemoveCharacter(this);
                }
                if (value != null)
                {
                    value.AddCharacter(this);
                }
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void NotifyChange()
        {
            HasChanged?.Invoke(this, EventArgs.Empty);
        }
        private void RaiseIsRecordedChanged()
        {
            IsRectordedChanged?.Invoke(this, _isRecorded);
        }


        public Character()
        {
            //_voiceTalent = new VoiceTalent();
        }
        public Character(Project project) : this()
        {
            Project = project;
        }
        public Character(string n, Line l)
        {
            Name = n;
            Comment = string.Empty;
            Status = true;
            AddLine(l);
            //VoiceTalent = null;
        }
        public Character(string n, List<Line> lines)
        {
            Name = n;
            Comment = string.Empty;
            Status = true;
            AddLines(lines);
        }
        public Character(Character character)
        {
            _name = character.Name;
            _comment = character.Comment;
            _status = character.Status;
            _voiceTalent = character.VoiceTalent;
            _project = character.Project;
            RaisePropertyChanged(nameof(Name));
            RaisePropertyChanged(nameof(Comment));
            RaisePropertyChanged(nameof(Status));
            RaisePropertyChanged(nameof(VoiceTalent));
            RaisePropertyChanged(nameof(Project));
        }


        public event EventHandler HasChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public event IsRecordedChanged IsRectordedChanged;
        
        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Character left, Character right)
        {
            if (ReferenceEquals(left, null))
            {
                if (ReferenceEquals(right, null))
                {
                    return true;
                }
                return false;
            }
            if (!ReferenceEquals(right, null))
            {
                return left.CharacterId.Equals(right.CharacterId);
            }
            return false;
        }
        public static bool operator !=(Character left, Character right)
        {
            if (ReferenceEquals(left, null))
            {
                if (ReferenceEquals(right, null))
                {
                    return false;
                }
                return true;
            }
            if (!ReferenceEquals(right, null))
            {
                return !left.CharacterId.Equals(right.CharacterId);
            }
            return true;
        }
    }
}
