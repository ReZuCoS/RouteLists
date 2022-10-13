using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace RouteLists.ViewModel
{
    internal class VehicleNumberConverter : IValueConverter
    {
        private Regex VehicleNumberRegex => new Regex("([АВЕКМНОРСТУХ]\\s*\\d{3}\\s*[АВЕКМНОРСТУХ]{2}\\s*\\d{2,3})$");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Length > 0 && !VehicleNumberRegex.IsMatch(value.ToString().ToUpper());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
