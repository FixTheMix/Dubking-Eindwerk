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
    class ProjectAccessToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (ProjectModuleAccess)value;
            switch (input)
            {
                case ProjectModuleAccess.ReadOnly:
                    return "Read Only";
                case ProjectModuleAccess.ReadWrite:
                    return "Read and Write";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (string)value;
            switch (input)
            {
                case "Read Only" :
                    return ProjectModuleAccess.ReadOnly;
                case "Read and Write":
                    return ProjectModuleAccess.ReadWrite;
                default:
                    return null;
            }
        }
    }
}
