using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace RouteLists.ViewModel
{
    internal class OnlyLettersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value.ToString().All(char.IsLetter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
