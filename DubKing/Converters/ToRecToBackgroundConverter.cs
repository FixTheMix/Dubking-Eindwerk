using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DubKing.Converters
{
    public class ToRecToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.GetType();
            if (type == typeof(int))
            {
                var input = (int)value;
                if (input == 0)
                {
                    return Application.Current.Resources["Green"];
                }
            }
            else
            {
                var input = (double)value;
                if (input == 0)
                {
                    return Application.Current.Resources["Green"];
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
