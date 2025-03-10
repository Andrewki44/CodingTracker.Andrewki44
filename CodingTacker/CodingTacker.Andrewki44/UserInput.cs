using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    static class UserInput
    {
        public static string MainMenu()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold red]~~~ Main Menu ~~~[/]?")
                    .PageSize(5)
                    .HighlightStyle(new Style().Foreground(Color.Green))
                    .AddChoices(menuOptions)
            );
        }

        private static string[] menuOptions = new string[]
        {
            "Start a Session",
            "Manually Record a Session",
            "Reports"
        };
    }
}
