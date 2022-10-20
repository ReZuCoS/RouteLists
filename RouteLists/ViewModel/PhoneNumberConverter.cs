using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace RouteLists.ViewModel
{
    internal class PhoneNumberConverter : IValueConverter
    {
        private Regex PhoneNumberRegex => new Regex("^(\\s*)?(\\+)?([- _():=+]?\\d[- _():=+]?){10,14}(\\s*)?$");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().Length > 0 && !PhoneNumberRegex.IsMatch(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
