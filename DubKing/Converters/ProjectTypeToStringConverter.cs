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
    class ProjectTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(ProjectType))
            {
                return null;
            }
            var input = (ProjectType)value;
            switch (input)
            {
                case ProjectType.LiveActionFilm:
                    return "Live Action Film";
                case ProjectType.LiveActionSeries:
                    return "Live Action Series";
                case ProjectType.AnimationFilm:
                    return "Animation Film";
                case ProjectType.AnimationSeries:
                    return "Animation Series";
                case ProjectType.Other:
                    return "Other";
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
