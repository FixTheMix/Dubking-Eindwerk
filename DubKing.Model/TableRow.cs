using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class TableRow : INotifyPropertyChanged
    {
        private Episode[] _episodes = new Episode[0];
        private Character _character;
        private LineCount[] _lineCounts = new LineCount[0];

        public event PropertyChangedEventHandler PropertyChanged;

        public Episode[] Episodes { get => _episodes; set => AddEpsiodes(value); }
        public Character Character { get => _character; private set => _character = value; }
        public LineCount[] LineCounts { get => _lineCounts; }

        public int TotalLineCount { get { return GetTotal(); } }
        public double TotalAvgCount { get { return GetTotalAvg(); } }
        public double TotalEwlCount { get { return GetTotalEwl(); } }
        private int GetTotal()
        {
            return LineCounts.Select(l => l.Lines).Sum();
        }
        private double GetTotalAvg()
        {
            return Math.Round(LineCounts.Select(_ => _.Avg).Sum());
        }
        private double GetTotalEwl()
        {
            return Math.Round(LineCounts.Select(_ => _.Ewl).Sum());
        }
        public TableRow(Character character)
        {
            _character = character;
        }

        public void AddEpisode(Episode episode)
        {
            if (!_episodes.Select(_=>_.EpisodeId).Contains(episode.EpisodeId))
            {
                var existingEpisodes = _episodes.ToList<Episode>();
                existingEpisodes.Add(episode);
                _episodes = existingEpisodes.OrderBy(_ => _.EpisodeId).ToArray();
                if (episode.Characters.ContainsKey(_character))
                {
                    AddLineCount(episode.Characters[_character]);
                }
                else
                {
                    AddLineCount(new LineCount(episode));
                }
            }
            else
            {
                int index = 0;
                for (int i = 0; i < _episodes.Length; i++)
                {
                    if (_episodes[i] == episode)
                    {
                        index = i;
                        break;
                    }
                }
                if (episode.Characters.ContainsKey(_character))
                {
                    if (episode.Characters[_character] != _lineCounts[index])
                    {
                        var existingLineCounts = _lineCounts.ToList();
                        existingLineCounts[index] = episode.Characters[_character];
                        _lineCounts = existingLineCounts.OrderBy(_ => _.Episode.EpisodeId).ToArray();
                    }
                }
            }
        }
        public void AddEpsiodes(IEnumerable<Episode> episodes)
        {
            var items = episodes.ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                AddEpisode(items[i]);
            }
        }
        private void AddLineCount(LineCount lineCount)
        {
            var existingLineCounts = _lineCounts.ToList();
            existingLineCounts.Add(lineCount);
            _lineCounts = existingLineCounts.OrderBy(_ => _.Episode.EpisodeId).ToArray();
        }
        public void RemoveEpisode(Episode episode)
        {
            var existing = _episodes.ToList();
            existing.Remove(episode);
            _episodes = existing.ToArray();
            RaisePropertyChanged(nameof(Episodes));
        }
        private void RaisePropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
