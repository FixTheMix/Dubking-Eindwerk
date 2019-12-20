using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace DubKing.Utils
{
    public class GridPosition: ObservableObject
    {
        int _column;
        int _row;
        int _rowSpan;
        int _columnSpan;
        double _rotation;
        #region Properties
        
        public int Column
        {
            get { return _column; }
            set
            {
                if (_column == value) return;
                _column = value;
                RaisePropertyChanged();
            }
        }

        public int Row
        {
            get { return _row; }
            set
            {
                if (_row == value) return;
                _row = value;
                RaisePropertyChanged();
            }
        }

        public int ColumnSpan
        {
            get { return _columnSpan; }
            set
            {
                if (_columnSpan == value) return;
                _columnSpan = value;
                RaisePropertyChanged();
            }
        }

        public int RowSpan
        {
            get { return _rowSpan; }
            set
            {
                if (_rowSpan == value) return;
                _rowSpan = value;
                RaisePropertyChanged();
            }
        }

        public double Rotation
        {
            get { return _rotation; }
            set
            {
                if (_rotation == value) return;
                _rotation = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        public GridPosition()
        {
            Column = 0;
            Row = 0;
            ColumnSpan = 1;
            RowSpan = 1;
            Rotation = 0;
        }
        public GridPosition(int c, int r, int cS,int rS, double a)
        {
            Column = c;
            Row = r;
            ColumnSpan = cS;
            RowSpan = rS;
            Rotation = a;
        }
        public void SetPosition(int c, int r, int cS, int rS, double a)
        {
            Column = c;
            Row = r;
            ColumnSpan = cS;
            RowSpan = rS;
            Rotation = a;
        }
    }
}
