using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                //string menu = Menu.MainMenu();

                switch (Menu.MainMenu())
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
                                Validation.SaveSessionError(e);
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
                                Validation.SaveSessionError(e);
                            }
                        }
                        break;

                    case "Reports":
                        switch (Menu.ReportMenu()) 
                        {
                            case "View Logs":
                                try
                                {
                                    CodingSession log = Menu.LogMenu(SQLite.GetCodingSessions());
                                    try
                                    {
                                        Menu.ActionMenu(log);
                                    }
                                    catch(Exception e)
                                    {

                                    }
                                }
                                catch (Exception e)
                                {
                                    Validation.GetSessionsError(e);
                                }
                                break;

                            case "Return to Main Menu":
                                continue;
                        }
                        break;
                }
            } while (true);
        }
    }
}
