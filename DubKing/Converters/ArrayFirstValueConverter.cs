using System;
using System.Globalization;
using System.Windows.Data;

namespace DubKing.Converters
{
    class ArrayFirstValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (string[])value;
            return input[0].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
