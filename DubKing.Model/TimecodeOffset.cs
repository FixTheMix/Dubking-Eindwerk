using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Model
{
    public class TimecodeOffset:Timecode, INotifyPropertyChanged
    {
        #region Fields
        private char _sign;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Properties
        public char Sign
        {
            get
            {
                return _sign;
            }
            set
            {
                if (IsValidSign(value))
                {
                    _sign = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sign)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OffsetValue)));
                    
                }
                else
                {
                    throw new Exception("Unvalid sign.");
                }
            }
        }
        public int OffsetValue
        {
            get
            {
                switch (Sign)
                {
                    case '+':
                        return this.TotalFrames;
                    case '-':
                        return this.TotalFrames * -1;
                    default:
                        throw new Exception("This one Can't Occur");
                }
            }
            set
            {
                if (value < 0)
                {
                    Sign = '-';
                    this.Frame = value * -1;
                }
                else
                {
                    Sign = '+';
                    this.Frame = value;
                }
            }
        }
        #endregion
        #region Constructors
        public TimecodeOffset(FrameRate framerate = FrameRate.fps25):base(framerate)
        {
            _sign = '+';
        }
        public TimecodeOffset():base(FrameRate.fps25)
        {
            _sign = '+';
        }
        
        #endregion

        #region Methods
        private bool IsValidSign(char sign)
        {
            switch (sign)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                default:
                    return false;
            }
        }
        public override string ToString()
        {
            return $"{Sign}{base.ToString()}";
        }
        #endregion
        #region Operators

        public static explicit operator TimecodeOffset(int i)
        {
            var tc = new TimecodeOffset(FrameRate.fps25) { Frame = i };
            return tc;
        }
        public static TimecodeOffset operator +(TimecodeOffset ltc, TimecodeOffset rtc)
        {
            if (ltc.FrameRate != rtc.FrameRate)
            {
                throw new Exception("Framerates should be equal.");
            }
            else
            {
                var result = new TimecodeOffset(ltc.FrameRate);
                result.Frame = ltc.OffsetValue + rtc.OffsetValue;
                return result;
            }
        }
        public static Timecode operator +(Timecode ltc, TimecodeOffset rtc)
        {
            if (ltc.FrameRate != rtc.FrameRate)
            {
                throw new Exception("Framerates should be equal.");
            }
            else
            {
                var result = new Timecode(ltc.FrameRate);
                result.Frame = ltc.TotalFrames + rtc.OffsetValue;
                return result;
            }
        }
        public static TimecodeOffset operator -(TimecodeOffset ltc, TimecodeOffset rtc)
        {
            if (ltc.FrameRate != rtc.FrameRate)
            {
                throw new Exception("Framerates should be equal.");
            }
            else
            {
                var result = new TimecodeOffset(ltc.FrameRate);
                result.Frame = ltc.OffsetValue - rtc.OffsetValue;
                return result;
            }
        }
        public static Timecode operator -(Timecode ltc, TimecodeOffset rtc)
        {
            if (ltc.FrameRate != rtc.FrameRate)
            {
                throw new Exception("Framerates should be equal.");
            }
            else
            {
                var result = new Timecode(ltc.FrameRate);
                result.Frame = ltc.TotalFrames - rtc.OffsetValue;
                return result;
            }
        }
        #endregion
    }
}
