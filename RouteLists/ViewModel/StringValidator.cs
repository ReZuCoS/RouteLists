using System.Linq;
using System.Text.RegularExpressions;

namespace RouteLists.ViewModel
{
    public class StringValidator
    {
        private static Regex VehicleNumberRegex => new Regex("([АВЕКМНОРСТУХ]\\s*\\d{3}\\s*[АВЕКМНОРСТУХ]{2}\\s*\\d{2,3})$");
        private static Regex PhoneNumberRegex => new Regex("^(\\s*)?(\\+)?([- _():=+]?\\d[- _():=+]?){11}(\\s*)?$");
        private static Regex CostRegex => new Regex("^[0-9]*(\\,)?[0-9][0-9]?$");

        public static bool IsCorrectVehicleNumber(string input)
        {
            return VehicleNumberRegex.IsMatch(input.ToUpper());
        }

        public static bool IsCorrectPhoneNumber(string input)
        {
            return PhoneNumberRegex.IsMatch(input.ToUpper());
        }

        public static bool IsDigitsOnly(string input)
        {
            return input.All(char.IsDigit);
        }

        public static bool IsLettersOnly(string input)
        {
            return input.All(char.IsLetter);
        }

        public static bool IsCorrectCost(string input)
        {
            return CostRegex.IsMatch(input);
        }
    }
}
