using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DubKing.Converters
{
    public class DisplayOptionToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (DisplayOptions)value;
            var prm = (string)parameter;
            DisplayOptions opt;
            switch (Int16.Parse(prm))
            {
                case 0:
                    opt = DisplayOptions.Avg;
                    break;
                case 1:
                    opt = DisplayOptions.Ewl;
                    break;
                default:
                    opt = DisplayOptions.Tc;
                    break;
            }

            if (input == opt)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
