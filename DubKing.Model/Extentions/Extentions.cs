using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DubKing.Model.Extentions
{
    public static class Extentions
    {
        //TimeCode Extentions
        public static string FullTimeCode(this string tc)
        {

            tc = tc.Replace(":", "");

            switch (tc.Length)
            {
                case 0:
                    return "00:00:00:00";
                case 1:
                    return $"00:00:00:0{tc}";
                case 2:
                    return $"00:00:00:{tc}";
                case 3:
                    return $"00:00:0{tc.Substring(0, 1)}:{tc.Substring(1, 2)}";
                case 4:
                    return $"00:00:{tc.Substring(0, 2)}:{tc.Substring(2, 2)}";
                case 5:
                    return $"00:0{tc.Substring(0, 1)}:{tc.Substring(1, 2)}:{tc.Substring(3, 2)}";
                case 6:
                    return $"00:{tc.Substring(0, 2)}:{tc.Substring(2, 2)}:{tc.Substring(4, 2)}";
                case 7:
                    return $"0{tc.Substring(0, 1)}:{tc.Substring(1, 2)}:{tc.Substring(3, 2)}:{tc.Substring(5, 2)}";
                case 8:
                    return $"{tc.Substring(0, 2)}:{tc.Substring(2, 2)}:{tc.Substring(4, 2)}:{tc.Substring(6, 2)}";
                default:
                    tc = tc.Substring(tc.Length - 8);
                    return $"{tc.Substring(0, 2)}:{tc.Substring(2, 2)}:{tc.Substring(4, 2)}:{tc.Substring(6, 2)}";
            }
        }
        public static string TimeFormat(this string time)
        {
            time = time.RemoveNonDigit();

            switch (time.Length)
            {
                case 0:
                    return "00:00:00:00";
                case 1:
                    return $"00:00:00:0{time}";
                case 2:
                    return $"00:00:00:{time}";
                case 3:
                    return $"00:00:0{time.Substring(0, 1)}:{time.Substring(1, 2)}";
                case 4:
                    return $"00:00:{time.Substring(0, 2)}:{time.Substring(2, 2)}";
                case 5:
                    return $"00:0{time.Substring(0, 1)}:{time.Substring(1, 2)}:{time.Substring(3, 2)}";
                case 6:
                    return $"00:{time.Substring(0, 2)}:{time.Substring(2, 2)}:{time.Substring(4, 2)}";
                
                default:
                    time = time.Substring(time.Length - 6);
                    return $"{time.Substring(0, 2)}:{time.Substring(2, 2)}:{time.Substring(4, 2)}";
            }
        }
        public static string RemoveNonDigit(this string input)
        {
            return Regex.Replace(input, "[^0-9]", "");
        }
        public static string PhoneNumber(this string input)
        {
            return Regex.Replace(input, "[/w]", "");
        }
        public static Timecode ParseTimeCode(this string tc, FrameRate frameRate)
        {
            var tcValues = tc.Split(':');
            var result = new Timecode(frameRate)
            {
                Hour = Int32.Parse(tcValues[0]),
                Minute = Int32.Parse(tcValues[1]),
                Second = Int32.Parse(tcValues[2]),
                Frame = Int32.Parse(tcValues[3]),
            };
            return result;
        }
        public static string FullOffset(this string tc)
        {
            string sign;
            if (tc.Length == 0)
            {
                sign = "+";
            }
            else
            {
                sign = tc.Substring(0, 1);
                switch (sign)
                {
                    case "+":
                        break;
                    case "-":
                        break;
                    default:
                        sign = "+";
                        break;
                }
            }
            

            string timecode = tc.RemoveNonDigit().FullTimeCode();

            
            return sign + timecode;
        }
        public static TimecodeOffset ParseToOffset(this string tc, FrameRate frameRate)
        {
            var tcValues = tc.Substring(1).Split(':');
            var sign = tc[0];
            switch (sign)
            {
                case '+':
                    break;
                case '-':
                    break;
                default:
                    sign = '+';
                    break;
            }
            var result = new TimecodeOffset(frameRate)
            {
                Sign = tc[0],
                Hour = Int32.Parse(tcValues[0]),
                Minute = Int32.Parse(tcValues[1]),
                Second = Int32.Parse(tcValues[2]),
                Frame = Int32.Parse(tcValues[3])
            };
            return result;
        }


        //String extentions
        public static string ToTitleCase(this string value)
        {
            if (value == string.Empty)
            {
                return value;
            }
            var words = value.Trim().Split(' ');
            string result = string.Empty;
            foreach (var word in words)
            {
                result += word.Substring(0, 1).ToUpper() + word.Substring(1);
                result += " ";
            }
            return result.TrimEnd();
        }
        public static int WordCount(this string value)
        {
            if (value == null) return 0;
            value = value.Replace("  ", " ");
            var words = value.Split(' ');

            return words.Length;
        }
        public static double ToDouble(this int value)
        {
            return (double)value;
        }
        public static int NullCheck(this object value)
        {
            if (value == null)
            {
                return 0;
            }
            return (int)value;
        }
    }
}
