using Spectre.Console;
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

        public static void SaveSessionError(Exception e)
        {
            AnsiConsole.MarkupLine("Failed to save session...");
            AnsiConsole.MarkupLine(e.Message);
            AnsiConsole.MarkupLine("Press Enter to return to menu...");
            Console.ReadLine();
        }

        public static void GetSessionsError(Exception e)
        {
            AnsiConsole.MarkupLine("Failed to retrieve sessions...");
            AnsiConsole.MarkupLine(e.Message);
            AnsiConsole.MarkupLine("Press Enter to return to menu...");
            Console.ReadLine();
        }

        public static void LogActionError(Exception e)
        {
            AnsiConsole.MarkupLine("Log action failed...");
            AnsiConsole.MarkupLine(e.Message);
            AnsiConsole.MarkupLine("Press Enter to return to menu...");
            Console.ReadLine();
        }
    }
}
