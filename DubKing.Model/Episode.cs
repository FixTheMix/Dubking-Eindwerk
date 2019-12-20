using DubKing.Model.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DubKing.Model
{
    public enum EpStatus { Recording, Mixing, Mastering, Delivering}
    public class Episode : INotifyChange, INotifyPropertyChanged
    {
        #region Fields
        private string _title;
        private int _number;
        private string _translator;
        private string _comment;
        private bool _customCodeToggle;
        private string _customCode;
        private Line[] _lines = new Line[0];
        private EpStatus _epStatus;
        private TimecodeOffset _offset;
        private Project _project;
        private Dictionary<Character, LineCount> _characters = new Dictionary<Character, LineCount>();
        #endregion
        #region Properties
        public int EpisodeId { get; set; }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                InvokePropertyChanged();
                NotifyChange();
            }
        }
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                InvokePropertyChanged();
                NotifyChange();
            }
        }
        public string Translator
        {
            get { return _translator; }
            set
            {
                _translator = value;
                InvokePropertyChanged();
                NotifyChange();
            }
        }
        public EpStatus EpStatus
        {
            get => _epStatus;
            set
            {
                if (_epStatus == value) return;
                _epStatus = value;
                NotifyChange();
                InvokePropertyChanged();
            }

        }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                InvokePropertyChanged();
                NotifyChange();
            }
        }
        public bool CustomCodeToggle
        {
            get { return _customCodeToggle; }
            set
            {
                _customCodeToggle = value;
                InvokePropertyChanged();
                NotifyChange();
            }
        }
        public string CustomCode
        {
            get { return _customCode; }
            set
            {
                _customCode = value;
                InvokePropertyChanged();
                NotifyChange();
            }
        }
        public Project Project { get => _project; set => SetProject(value); }
        public TimecodeOffset Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                RegisterPropertyChanged(_offset);
                RaisePropertyChanged(this, nameof(Offset));
                InvokePropertyChanged();
            }
        }
        public Line[] Lines { get => _lines; set => _lines = value; }
        public Dictionary<Character,LineCount> Characters => _characters;

        public event EventHandler HasChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        private void InvokePropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void RegisterPropertyChanged(INotifyPropertyChanged obj)
        {
            obj.PropertyChanged += RaisePropertyChangedChild;
        }
        private void RaisePropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void RaisePropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void RaisePropertyChangedChild(object sender, PropertyChangedEventArgs e)
        {
            foreach (var property in typeof(Episode).GetProperties())
            {
                if (property.GetValue(this) == sender)
                {
                    RaisePropertyChanged(this, property.Name);
                    return;
                }
            }
        }
        #region Methodes

        private void SetProject(Project p)
        {
            if (p == null) return;
            _project = p;
            p.AddEpisode(this);
        }
        public void AddLine(Line line)
        {
            if (!_lines.Contains(line))
            {
                line.PropertyChanged += CheckStatus;
                var existingLines = _lines.ToList();
                existingLines.Add(line);
                _lines = existingLines.ToArray();
            }

            if (line.Character != null)
            {
                AddCharacter(line.Character);
            }
            CheckStatus();
            RaisePropertyChanged(nameof(Lines));
        }
        public void RemoveLine(Line line)
        {
            line.PropertyChanged -= CheckStatus;
            var existing = _lines.ToList();
            existing.Remove(_lines.SingleOrDefault(_=>_.LineId == line.LineId));
            _lines = existing.ToArray();
            RemoveCharacter(line.Character);
            CheckStatus();
            RaisePropertyChanged(nameof(Lines));
        }
        public void RemoveCharacter(Character character)
        {
            if (character == null) return;
            if (!_lines.Select(_=>_.Character).Contains(character))
            {
                character.PropertyChanged -= CheckStatus;
                _characters.Remove(character);
            }
        }
        private void AddCharacter(Character character)
        {
            if (!_characters.ContainsKey(character))
            {
                character.PropertyChanged += CheckStatus;
                _characters.Add(character, new LineCount(this)
                {
                    Character = character,
                    Lines = _lines.Where(_ => _.Character == character).Count(),
                    Ewl = _lines.Where(_ => _.Character == character).Select(_ => _.Ewl).Sum(),
                    RecordedLines = _lines.Where(_ => _.Character == character && _.IsRecorded).Count(),
                    RecordedEwl = _lines.Where(_ => _.Character == character && _.IsRecorded).Select(_=>_.Ewl).Sum()
                });
            }
            else
            {
                _characters[character].Lines = _lines.Where(_ => _.Character == character).Count();
                _characters[character].Ewl = _lines.Where(_ => _.Character == character).Select(_ => _.Ewl).Sum();
                _characters[character].RecordedLines = _lines.Where(_ => _.Character == character && _.IsRecorded).Count();
                _characters[character].RecordedEwl = _lines.Where(_ => _.Character == character && _.IsRecorded).Select(_ => _.Ewl).Sum();
            }
            if(_project != null)_project.AddEpisode(this);
        }
        private void CheckStatus(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Status" && e.PropertyName != "IsRecorded") return;
            CheckStatus();

        }
        private void CheckStatus()
        {
            if (EpStatus > EpStatus.Mixing) return;
            if (_lines.Where(_ => _.Character.Status == true).Where(_ => _.IsRecorded == false).Count() == 0)
            {
                EpStatus = EpStatus.Mixing;
            }
            else
            {
                EpStatus = EpStatus.Recording;
            }
        }
        private void NotifyChange()
        {
            HasChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Constuctor
        public Episode(Project project) : this()
        {
            _project = project;
            _project.AddEpisode(this);
            _offset.FrameRate = _project.FrameRate;
        }
        public Episode()
        {
            _epStatus = EpStatus.Recording;
            _offset = new TimecodeOffset(FrameRate.fps25);
        }
        #endregion

        public static bool operator ==(Episode left, Episode right)
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
                return left.EpisodeId.Equals(right.EpisodeId);
            }
            return false;
        }
        public static bool operator !=(Episode left, Episode right)
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
                return !left.EpisodeId.Equals(right.EpisodeId);
            }
            return true;
        }

        public override string ToString()
        {
            return $"{EpisodeId} - {CustomCode} - {Title} - {_project.Title} - {Project.Customer}";
        }
    }
}
