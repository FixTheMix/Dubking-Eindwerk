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
    class FrameRateStringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(FrameRate))
            {
                return null;
            }
            var input = (FrameRate)value;
            switch (input)
            {
                case FrameRate.fps24:
                    return "24 fps";
                case FrameRate.fps25:
                    return "25 fps";
                case FrameRate.fps24_98:
                    return "24,98 fps";
                case FrameRate.fps29_98:
                    return "29,98 fps";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
