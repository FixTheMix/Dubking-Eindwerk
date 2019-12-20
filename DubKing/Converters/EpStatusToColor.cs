using DubKing.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DubKing.Converters
{
    public class EpStatusToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (EpStatus)value;
            switch (status)
            {
                case EpStatus.Recording:
                    return Application.Current.Resources["Status_Red"];
                case EpStatus.Mixing:
                    return Application.Current.Resources["Status_Orange"]; ;
                case EpStatus.Mastering:
                    return Application.Current.Resources["Status_Yellow"]; ;
                case EpStatus.Delivering:
                    return Application.Current.Resources["Status_Green"]; ;
                default:
                    return Application.Current.Resources["Status_Red"]; ;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
