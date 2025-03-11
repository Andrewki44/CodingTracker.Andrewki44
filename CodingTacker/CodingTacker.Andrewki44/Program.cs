using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            do
            {
                string menu = Menu.MainMenu();

                switch (menu)
                {
                    case "Start a Session":
                        {
                            try
                            {
                                CodingSession session = new CodingSession().StartSession();
                                if (Menu.ConfirmSave(session))
                                    SQLite.SaveCodingSession(session);
                            }
                            catch (Exception e)
                            {
                                Menu.ErrorMenu(e);
                            }
                        }
                        break;

                    case "Manually Record a Session":
                        {
                            try
                            {
                                CodingSession session = new CodingSession().ManualSession();
                                if (Menu.ConfirmSave(session))
                                    SQLite.SaveCodingSession(session);
                            }
                            catch (Exception e)
                            {
                                Menu.ErrorMenu(e);
                            }
                        }
                        break;

                    case "Reports":
                        break;
                }

            } while (true);
        }
    }
}
