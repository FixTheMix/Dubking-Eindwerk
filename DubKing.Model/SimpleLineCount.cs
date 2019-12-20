using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class SimpleLineCount : INotifyPropertyChanged
    {
        private int _recordedLines;
        private double _recordedEwl;

        public event PropertyChangedEventHandler PropertyChanged;

        public int RecordedLines { get => _recordedLines; set { _recordedLines = value; RaisePropertyChanged(); } }
        public int TotalLines { get; set; }
        public int NotRecordedLines { get => TotalLines - RecordedLines; }
        public double RecordedEwl { get => _recordedEwl; set { _recordedEwl = value; RaisePropertyChanged(); } }
        public double TotalEwl { get; set; }
        public double NotRecordedEwl { get => TotalEwl - RecordedEwl; }
        public double RecordedAvg { get => (RecordedEwl + RecordedLines) / 2; }
        public double TotalAvg { get => (TotalEwl + TotalLines) / 2; }
        public double NotRecordedAvg { get => TotalAvg - RecordedAvg; }

        private void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void RaisePropertyChangedForAll()
        {
            RaisePropertyChanged(nameof(RecordedLines));
            RaisePropertyChanged(nameof(NotRecordedLines));
            RaisePropertyChanged(nameof(TotalLines));
            RaisePropertyChanged(nameof(RecordedAvg));
            RaisePropertyChanged(nameof(NotRecordedAvg));
            RaisePropertyChanged(nameof(TotalAvg));
            RaisePropertyChanged(nameof(RecordedEwl));
            RaisePropertyChanged(nameof(NotRecordedEwl));
            RaisePropertyChanged(nameof(TotalEwl));
        }


    }
}
