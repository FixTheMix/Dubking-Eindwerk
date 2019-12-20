using DubKing.Helpers;
using DubKing.Messages;
using DubKing.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DubKing.ViewModel.ProjectTable
{
    public class TableRowViewModel:ViewModelBase
    {
        #region Fields
        private Character _character;
        
        
        private static List<Episode> _episodes;
        private static IEnumerable<Episode> _filteredEpisodes;
        private ObservableCollection<LineCount> _lineCounts;
        #endregion
        #region Properties

        public TableRow TableRow { get; set; }
        public static List<Episode> Episodes
        {
            get => _episodes;
            set => _episodes = value;
        }
        public static IEnumerable<Episode> FilteredEpisodes
        {
            get => _filteredEpisodes == null ? _episodes : _filteredEpisodes;
            set
            {
                _filteredEpisodes = value;
            }
        }
        public Character Character  
        {
            get { return _character; }
            set { Set(ref _character, value); }
        }
        
        
        
        public ObservableCollection<LineCount> LineCounts { get => _lineCounts ?? (_lineCounts = GetLineCount()); }
        //public IEnumerable<object> LineCountDisplay
        //{
        //    get
        //    {
        //        switch (_display)
        //        {
        //            case DisplayOptions.Avg:

        //                return LineCounts.Select(lc => new LineCountViewModel { Display = lc.AvgDisplay, ToRec = lc.NotRecordedAvg, Character = Character, Episode = lc.Episode, LineCount = lc });
        //                break;
        //            case DisplayOptions.Ewl:
        //                return LineCounts.Select(lc => new LineCountViewModel { Display = lc.EwlDisplay, ToRec = lc.NotRecorderEwl, Character = Character, Episode = lc.Episode, LineCount = lc });
        //                break;
        //            case DisplayOptions.Tc:
        //                return LineCounts.Select(lc => new LineCountViewModel { Display = lc.LineDisplay, ToRec = lc.NotRecordedLines, Character = Character, Episode = lc.Episode, LineCount = lc });
        //                break;
        //            default:
        //                return LineCounts.Select(lc => new LineCountViewModel { Display = lc.AvgDisplay, ToRec = lc.NotRecordedAvg, Character = Character, Episode = lc.Episode, LineCount = lc });
        //                break;
        //        }
        //    }
        //}

        public event EventHandler DoubleClickCharacterEvent;
        public event EventHandler DoubleClickActorEvent;
        private DisplayOptions _display = DisplayOptions.Avg;

        public DisplayOptions Display
        {
            get { return _display; }
            set
            {
                if (_display != value)
                {
                    _display = value;
                    //RaisePropertyChanged(nameof(LineCountDisplay));
                }
            }
        }


        #endregion

        #region Constructor
        public TableRowViewModel(List<Episode> episodes, Character character)
        {
            _episodes = episodes;
            Character = character;
            Character.PropertyChanged += UpdateDisplay;
        }
        public TableRowViewModel(TableRow t)
        {
            TableRow = t;
            Character = t.Character;
            Episodes = t.Episodes.ToList();

            RaisePropertyChanged(nameof(TableRow));
        }
        
        private void UpdateDisplay(object obj, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Status")
            {
                //RaisePropertyChanged(nameof(LineCounts));
            }
            
        }

        public void FilterColumns(IEnumerable<Episode> episodes)
        {
            FilteredEpisodes = episodes;
            //RaisePropertyChanged(nameof(LineCounts));
            //RaisePropertyChanged(nameof(TotalLineCount));
            //RaisePropertyChanged(nameof(LineCountDisplay));
        }
        #endregion

        #region Methodes

        #endregion

        #region Commands
        #region Select Actor
        

        private ObservableCollection<LineCount> GetLineCount()
        {
            var lineCounts = new ObservableCollection<LineCount>(TableRow.LineCounts);

            //Episode[] episodes = FilteredEpisodes.ToArray();

            //for (int i = 0; i < episodes.Length; i++)
            //{
            //    LineCount linecount;
            //    if (episodes[i].Project.LineCounts.ContainsKey(Character.CharacterId) && episodes[i].Project.LineCounts[Character.CharacterId].ContainsKey(episodes[i].EpisodeId))
            //    {
            //        linecount = episodes[i].Project.LineCounts[Character.CharacterId][episodes[i].EpisodeId];
            //    }
            //    else
            //    {
            //        linecount = new LineCount();
            //    }
            //    linecount.Episode = episodes[i];
            //    linecount.Character = Character;
            //    lineCounts.Add(linecount);
            //}

            return lineCounts;
        }
        #endregion
        #endregion

        public override string ToString()
        {
            return Character.Name;
        }
        
    }
}
