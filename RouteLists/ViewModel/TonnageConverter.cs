using System;
using System.Globalization;
using System.Windows.Data;

namespace RouteLists.ViewModel
{
    internal class TonnageConverter : IValueConverter
    {
        public int TonnageToPass { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _ = int.TryParse(value.ToString(), out int tonnage);

            return tonnage >= TonnageToPass;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
