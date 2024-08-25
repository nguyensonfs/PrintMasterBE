using System.Text.RegularExpressions;

namespace PrintMaster.Commons.Utils
{
    public static class ValidationUtilities
    {
        public static bool IsPasswordValid(this string password)
        {
            var pattern = @"^\S{6,}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static bool IsPhoneNumberValid(this string phone)
        {
            var pattern = @"^0[0-9]{9}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(phone);
        }

        public static bool IsEmailValid(this string email)
        {
            var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
