using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                /* TODO:
                 * Menu.ActionMenu() - Update / Delete logs
                 * Other Reports
                 */

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

                                    //if null CodingSession, return to Main Menu
                                    if (!log.sessionStart.HasValue)
                                        break;
                                    else
                                    {
                                        try
                                        {
                                            Menu.ActionMenu(log);
                                        }
                                        catch (Exception e)
                                        {

                                        }
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

                    case "Exit":
                        return;
                }
            } while (true);
        }
    }
}
