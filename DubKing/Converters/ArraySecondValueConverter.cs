using System;
using System.Globalization;
using System.Windows.Data;

namespace DubKing.Converters
{
    class ArraySecondValueConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (string[])value;
            if (input.Length > 1)
            {
                return input[1];
            }
            return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
