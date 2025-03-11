using Spectre.Console;
using System.Globalization;

namespace CodingTacker.Andrewki44
{
    static class Menu
    {
        public static string MainMenu()
        {
            Console.Clear();
            
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]~~~ Main Menu ~~~[/]")
                    .PageSize(5)
                    .HighlightStyle(new Style().Foreground(Color.Green))
                    .AddChoices(menuOptions)
            );
        }

        public static void ErrorMenu(Exception e)
        {
            AnsiConsole.MarkupLine("Failed to save session...");
            AnsiConsole.MarkupLine(e.Message);
            AnsiConsole.MarkupLine("Press Enter to return to menu...");
            Console.ReadLine();
        }

        public static bool ConfirmSave(CodingSession session)
        {
            bool confirmation =  AnsiConsole.Prompt(
                new TextPrompt<bool>("[blue]Save Session with duration: " +
                    $"{TimeSpan.FromSeconds(Math.Round(session.duration.TotalSeconds))}?[/]")
                    .AddChoice(true)
                    .AddChoice(false)
                    .DefaultValue(true)
                    .WithConverter(choice => choice ? "y" : "n"));

            //Show confirmation
            AnsiConsole.MarkupLine(confirmation ? "[green]Confirmed[/]" : "[red]Declined[/]");
            AnsiConsole.Markup((confirmation ? "[green]" : "[red]") + "Press Enter to return to the menu:[/]");
            Console.ReadLine();
            
            return confirmation;
        }

        public static (DateTime, DateTime) ManualSessionMenu()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold red]~ Manual Session Entry ~[/]");

            //Get startTime with validation
            DateTime startTime = DateTime.ParseExact(
                AnsiConsole.Prompt(
                    new TextPrompt<string>("[bold green]Enter your session start time [grey](yyyy-MM-dd HH:mm:ss)[/]:[/]")
                        .Validate(n => Validation.ValidateDateTimeExact(n))
                        .ValidationErrorMessage("[red]Input is not a valid DateTime[/]")
                ),
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture);

            //Get endTime with validation
            DateTime endTime = DateTime.ParseExact(
                AnsiConsole.Prompt(
                    new TextPrompt<string>("[bold green]Enter your session end time [grey](yyyy-MM-dd HH:mm:ss | blank = Now)[/]:[/]")
                        .DefaultValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        .HideDefaultValue()
                        .Validate(n => Validation.ValidateDateTimeExact(n))
                        .ValidationErrorMessage("[red]Input is not a valid DateTime[/]")
                ),
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture);
            
            return (startTime, endTime);
        }

        private static string[] menuOptions = new string[]
        {
            "Start a Session",
            "Manually Record a Session",
            "Reports"
        };
    }
}
