using DubKing.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.ViewModel.ProjectTable
{
    public class LineCountViewModel:ViewModelBase
    {
        private string _display;

        public string Display
        {
            get { return _display; }
            set { Set(ref _display ,value); }
        }

        private double _toRec;

        public double ToRec
        {
            get { return _toRec; }
            set { Set(ref _toRec , value); }
        }

        private Character _character;

        public Character Character
        {
            get { return _character; }
            set { Set(ref _character , value); }
        }
        private Episode _episode;

        public Episode Episode
        {
            get { return _episode; }
            set { Set(ref _episode , value); }
        }

        private LineCount _lineCount;

        public LineCount LineCount
        {
            get { return _lineCount; }
            set { Set(ref _lineCount , value); }
        }



    }
}
