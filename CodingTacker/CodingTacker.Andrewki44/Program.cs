using System.Configuration;
using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var confirmation = AnsiConsole.Prompt(
            //new TextPrompt<bool>("Run prompt example?")
            //    .AddChoice(true)
            //    .AddChoice(false)
            //    .DefaultValue(true)
            //    .WithConverter(choice => choice ? "y" : "n"));

            //// Echo the confirmation back to the terminal
            //Console.WriteLine(confirmation ? "Confirmed" : "Declined");

            do
            {
                string menu = UserInput.MainMenu();

                AnsiConsole.MarkupLine(menu);
                Console.WriteLine(menu);
                //Console.ReadLine();
            } while (true);
            


            //CodingSession session = new CodingSession();
            //System.Threading.Thread.Sleep(5000);
            //session.SetSessionEnd(DateTime.Now);

            //SQLite.SaveCodingSession(session);


            //// Echo the fruit back to the terminal
            //AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
        }
    }
}
