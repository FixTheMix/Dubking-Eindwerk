using System;
using System.Globalization;
using System.Windows.Data;

namespace DubKing.Converters
{
    class ArrayThirdValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (string[])value;
            if (input.Length > 2)
            {
                return input[2];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
