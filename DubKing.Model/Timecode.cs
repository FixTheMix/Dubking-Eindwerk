using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DubKing.Model
{
    public enum FrameRate
    {
        fps24,
        fps25,
        fps24_98,
        fps29_98 
    }
    public class Timecode
    {
        #region Fields
        private int _frame;
        private int _second;
        private int _minute;
        #endregion

        #region Properties
        public int Hour { get; set; }
        public int Minute
        {
            get { return _minute; }
            set { SetMinutes(value); }
        }
        public int Second
        {
            get { return _second; }
            set { SetSeconds(value); }
        }
        public int Frame
        {
            get { return _frame; }
            set
            {
                SetFrames(value);
            }
        }
        public FrameRate FrameRate { get; set; }
        public int TotalFrames { get { return GetTotalFrames(); } }
        #endregion
        #region Constructor
        public Timecode(FrameRate frameRate)
        {
            //SetDefaultFrameRate();
            FrameRate = frameRate;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inserts Default Values into the properties
        /// </summary>
        private void SetDefaultValues(bool FrameRateToDefault = true)
        {
            Hour = 0;
            Minute = 0;
            Second = 0;
            Frame = 0;
            if (FrameRateToDefault)
            {
                SetDefaultFrameRate();
            }
        }
        private void SetDefaultFrameRate()
        {
            FrameRate = FrameRate.fps25;
        }
        /// <summary>
        /// Calulates the total number of frames for current timecode
        /// </summary>
        /// <returns>Integer representing the total numbers of frames</returns>
        private int GetTotalFrames()
        {
            int frameRate = GetNumberOfFramesPerSec(FrameRate);
            int result = (Hour*60)+Minute;
            result = (result * 60) + Second;
            result = (result * frameRate) + Frame;
            return result;
        }
        private int GetNumberOfFramesPerSec(FrameRate framerate)
        {
            switch (framerate)
            {
                case FrameRate.fps24:
                    return 24;
                    break;
                case FrameRate.fps25:
                    return 25;
                    break;
                case FrameRate.fps24_98:
                    return 25;
                    //Drop frame 1 and two every minute exept on 10th minute
                    break;
                case FrameRate.fps29_98:
                    return 30;
                    //Drop frame 1 and two every minute exept on 10th minute
                    break;
                default:
                    return 0;
                    break;
            }
        }
        private void SetFrames(int input)
        {
            if (input < GetNumberOfFramesPerSec(FrameRate)-1)
            {
                _frame = input;
            }
            else
            {
                Second += input / GetNumberOfFramesPerSec(FrameRate);
                _frame = input % GetNumberOfFramesPerSec(FrameRate);
            }
        }
        public void SetTotalFrames(int TotalFrames)
        {
            SetDefaultValues(false);
            Frame = TotalFrames;
        }
        private void SetSeconds(int input)
        {
            if (input < 60)
            {
                _second = input;
            }
            else
            {
                Minute += input / 60;
                _second = input % 60;
            }
        }
        private void SetMinutes(int input)
        {
            if (input < 60)
            {
                _minute = input;
            }
            else
            {
                Hour += input / 60;
                _minute = input % 60;
            }
        }
        private string DoubleDigit(int input)
        {
            if (input < 10)
            {
                return $"0{input}";
            }
            else
            {
                return input.ToString();
            }
        }
        public override string ToString()
        {
            return $"{DoubleDigit(Hour)}:{DoubleDigit(Minute)}:{DoubleDigit(Second)}:{DoubleDigit(Frame)}";
        }
        #endregion


        #region Operators
        public static Timecode operator +(Timecode ltc, Timecode rtc)
        {
            if (ltc.FrameRate != rtc.FrameRate)
            {
                throw new Exception("Framerates should be equal.");
            }
            else
            {
                var result = new Timecode(ltc.FrameRate);
                result.Frame = ltc.TotalFrames + rtc.TotalFrames;
                return result;
            }
        }
        public static Timecode operator -(Timecode ltc, Timecode rtc)
        {
            if (ltc.FrameRate != rtc.FrameRate)
            {
                throw new Exception("Framerates should be equal.");
            }
            else
            {
                var result = new Timecode(ltc.FrameRate);
                result.Frame = ltc.TotalFrames - rtc.TotalFrames;
                return result;
            }
        }

        public static explicit operator Timecode(int i)
        {
            var tc = new Timecode(FrameRate.fps25) { Frame = i };
            return tc;
        }
        #endregion
    }
}
