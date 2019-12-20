using DubKing.Model.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public delegate void IsRecordedChanged(object sender, bool isRecStatus);
    public class Line : INotifyPropertyChanged
    {
        private Character _character;
        private string _comment;
        private bool _isRecorded;
        private Character _originalCharacter;
        private Timecode _timecodeWithOffset;
        private Episode _episode;
        private int _characterId;
        private int _episodeID;
        private LineMemento _lineMemento;

        #region Properties
        public int LineId { get; set; }
        public string Text { get; set; }
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Comment)));
            }
        }
        public bool IsRecorded
        {
            get => _isRecorded;
            set
            {
                _isRecorded = value;
                RaiseIsRecordedChanged();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRecorded)));
            }
        }
        public string OriginalText { get; set; }
        public int OriginalCharacterId { get; set; }
        public Character OriginalCharacter
        {
            get => _originalCharacter;
            set
            {
                _originalCharacter = value;
                SetOriginalCharacterId();
            }
        }
        public Timecode OriginalTimecode { get; set; }
        public Timecode Timecode { get; set; }
        public Timecode TimecodeWithOffset { get => GetOffsetted(); set => Timecode = TimeCodeMinusOffset(value); }
        public int CharacterId
        {
            get
            {
                if (_character == null)
                {
                    return _characterId;
                }
                return _character.CharacterId;
            }
            set { _characterId = value; }
        }
        public Character Character
        {
            get => _character;
            set
            {
                SetCharacter(value);
                SetCharacterId();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Character)));
            }
        }
        public int EpisodeID
        {
            get
            {
                if (_episode == null)
                {
                    return _episodeID;
                }
                return _episode.EpisodeId;
            }
            set
            {
                _episodeID = value;
            }
        }
        public Episode Episode
        {
            get => _episode;
            set
            {
                SetEpisode(value);
                RegisterPropertyChanged(_episode);
            }
        }
        public Double Ewl { get => Text.WordCount().ToDouble() / 8.00; }
        public bool HasChanged
        {
            get
            {
                if (Character != _lineMemento.Character || Text != _lineMemento.Text || Timecode != _lineMemento.Timecode)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion


        private void SetCharacter(Character character)
        {
            if (character == null) return;
            if (_character != character)
            {
                var prevCharacter = _character;
                _character = character;
                UpdatChildren();
                if(prevCharacter != null)prevCharacter.RemoveLine(this);
            }
        }
        private void UpdatChildren()
        {
            if (_character != null && _episode != null)
            {
                _episode.AddLine(this);
                _character.AddLine(this);
            }
        }
        private void SetEpisode(Episode ep)
        {
            if (ep == null) return;
            if (_episode != ep)
            {
                var prevEpisode = _episode;
                _episode = ep;
                UpdatChildren();
                if(prevEpisode != null)prevEpisode.RemoveLine(this);
            }
        }

        #region Constructor
        public Line(Episode episode)
        {
            SetEpisode(episode);
            //AddLineToEpisode();
            Text = string.Empty;
            OriginalText = Text;
        }
        public Line(Episode episode, Character character, Timecode timecode) : this(episode)
        {
            SetCharacter(character);
            OriginalCharacter = character;
            Timecode = timecode;
            OriginalCharacter = character;
            OriginalTimecode = timecode;
        }
        public Line()
        {

        }
        private void AddLineToEpisode()
        {
            Episode.AddLine(this);
        }


        private void RegisterPropertyChanged(INotifyPropertyChanged obj)
        {
            obj.PropertyChanged += RaisePropertyChangedChild;
        }

        private void RaisePropertyChanged(object sender, string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaisePropertyChangedChild(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Offset")
            {
                RaisePropertyChanged(this, nameof(TimecodeWithOffset));
                return;
            }
        }

        public void Revert()
        {
            Character = _lineMemento.Character;
            Text = _lineMemento.Text;
            Timecode = _lineMemento.Timecode;
        }
        public void Save()
        {
            _lineMemento = new LineMemento(Character, Text, Timecode, Comment);
        }

        private Timecode GetOffsetted()
        {
            var tc = Timecode.TotalFrames;
            var offset = Episode.Offset.OffsetValue;
            if (offset < 0)
            {
                while ((offset * -1) > tc)
                {
                    var add = new Timecode(Timecode.FrameRate)
                    {
                        Hour = 24
                    };
                    tc += add.TotalFrames;
                }
            }

            var total = tc + offset;
            return new Timecode(Timecode.FrameRate) { Frame = total };
        }
        private Timecode TimeCodeMinusOffset(Timecode tc)
        {
            int frames = tc.TotalFrames - Episode.Offset.OffsetValue;
            return new Timecode(Timecode.FrameRate) { Frame = frames };
        }
        public void ResetOriginal()
        {
            Text = OriginalText;
            Timecode.SetTotalFrames(OriginalTimecode.TotalFrames);
            Character = OriginalCharacter;
        }
        private void SetOriginalCharacterId()
        {
            OriginalCharacterId = OriginalCharacter.CharacterId;
        }
        private void SetCharacterId()
        {
            CharacterId = Character.CharacterId;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event IsRecordedChanged IsRecordedChanged;

        private void RaiseIsRecordedChanged()
        {
            IsRecordedChanged?.Invoke(this, _isRecorded);
        }
        #endregion

        public override string ToString()
        {
            return $"{LineId} {Character.Name} - {Text}";
        }
        public static bool operator ==(Line left, Line right)
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
                return left.LineId.Equals(right.LineId);
            }
            return false;
        }
        public static bool operator !=(Line left, Line right)
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
                return !left.LineId.Equals(right.LineId);
            }
            return true;
        }


    }
}
