using System.Globalization;

namespace CodingTacker.Andrewki44
{
    static class Validation
    {
        public static bool ValidateDateTimeExact(string input)
        {
            if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, 0, out DateTime dt))
                return true;
            else
                return false;
        }

    }
}
