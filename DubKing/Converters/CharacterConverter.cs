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
    public class CharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as Character;
            if (input != null)
            {
                return input.Name;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(Character))
                {
                    return value;
                }
                else
                {
                    var input = (string)value;
                    return new Character { Name = input };
                }
            }
            return value;
        }
    }
}
