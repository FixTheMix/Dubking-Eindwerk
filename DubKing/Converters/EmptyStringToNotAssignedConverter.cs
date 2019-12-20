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
    class EmptyStringToNotAssignedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (VoiceTalent)value;
            if (input == null)
            {
                return "Not assigned";
            }
            if (input.FullName.Trim() == "")
            {
                return "Not assigned";
            }
            
            return input.FullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
