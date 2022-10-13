using System.Linq;
using System.Text.RegularExpressions;

namespace RouteLists.ViewModel
{
    public class StringValidator
    {
        private static Regex VehicleNumberRegex => new Regex("([АВЕКМНОРСТУХ]\\s*\\d{3}\\s*[АВЕКМНОРСТУХ]{2}\\s*\\d{2,3})$");

        public static bool IsCorrectVehicleNumber(string input)
        {
            return VehicleNumberRegex.IsMatch(input.ToUpper());
        }

        public static bool IsDigitsOnly(string input)
        {
            return input.All(char.IsDigit);
        }

        public static bool IsLettersOnly(string input)
        {
            return input.All(char.IsLetter);
        }
    }
}
