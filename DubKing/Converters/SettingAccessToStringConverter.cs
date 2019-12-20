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
    class SettingAccessToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (SettingsModuleAccess)value;
            switch (input)
            {
                case SettingsModuleAccess.ReadWrite:
                    return "Read and Write";
                case SettingsModuleAccess.NoAccess:
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
                case "Read and Write":
                    return SettingsModuleAccess.ReadWrite;
                case "No Access":
                    return SettingsModuleAccess.NoAccess;
                default:
                    return null;
            }
        }
    }
}
