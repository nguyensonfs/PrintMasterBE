using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PrintMaster.Commons.Utils
{
    public static class StringUltilities
    {
        public static string? Slugify(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return Regex.Replace(value, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
        public static Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }
        public static string GenerateCodeActive()
        {
            return "PrintMaster" + "_" + DateTime.Now.Ticks.ToString();
        }
        public static async Task<bool> IsStringInListAsync(string inputString, List<string> listString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }

            if (listString == null)
            {
                throw new ArgumentNullException(nameof(listString));
            }

            foreach (var str in listString)
            {
                if (await CompareStringAsync(inputString, str))
                {
                    return true;
                }
            }
            return false;
        }
        public static string GetEmailSuccessMessage(string emailAddress) => $"Email đã được gửi đến: {emailAddress}";
        public static string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
