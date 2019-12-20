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
    public class SortByConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (SortingOptions)value;
            var prm = (string)parameter;
            SortingOptions opt;
            switch (Int16.Parse(prm))
            {
                case 0:
                    opt = SortingOptions.Character;
                    break;
                case 1:
                    opt = SortingOptions.Actor;
                    break;
                default:
                    opt = SortingOptions.Quantity;
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
