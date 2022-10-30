using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace RouteLists.ViewModel
{
    internal class CostConverter : IValueConverter
    {
        private Regex CostRegex => new Regex("^[0-9]*(\\,)?[0-9][0-9]?$");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Length > 0 && !CostRegex.IsMatch(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
