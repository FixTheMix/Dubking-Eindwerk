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
    class MuduleAccessToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (ModuleAccess)value;
            switch (input)
            {
                case ModuleAccess.ReadOnly:
                    return "Read Only";
                case ModuleAccess.ReadWrite:
                    return "Read and Write";
                case ModuleAccess.NoAccess:
                    return "No Access";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (string)value;
            switch (input)
            {
                case "Read Only":
                    return ModuleAccess.ReadOnly;
                case "Read and Write":
                    return ModuleAccess.ReadWrite;
                case "No Access":
                    return ModuleAccess.NoAccess;
                default:
                    return null;
            }
        }
    }
}
