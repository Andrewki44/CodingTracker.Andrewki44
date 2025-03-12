using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {

                MainMenu:
                switch (Menu.MainMenu())
                {
                    case "Start a Session":
                        {
                            try
                            {
                                CodingSession session = new CodingSession().StartSession();
                                if (Menu.ConfirmSave(session))
                                    SQLite.InsertCodingSession(session);
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
                                    SQLite.InsertCodingSession(session);
                            }
                            catch (Exception e)
                            {
                                Validation.SaveSessionError(e);
                            }
                        }
                        break;

                    case "Reports":
                        ReportsMenu:
                        switch (Menu.ReportMenu())
                        {
                            case "View Logs":
                            LogMenu:
                                try
                                {
                                    CodingSession log = Menu.LogMenu(SQLite.GetCodingSessions());

                                    //if null CodingSession, return to Main Menu
                                    if (log.SessionStart.HasValue)
                                        try
                                        {
                                            switch (Menu.ActionMenu(log))
                                            {
                                                case "Update Log":
                                                    CodingSession session = new CodingSession().ManualSession();
                                                    if (Menu.ConfirmSave(session))
                                                        SQLite.UpdateCodingSession(log, session);
                                                    goto LogMenu;

                                                case "Delete Log":
                                                    if (Menu.ConfirmDelete(log))
                                                        SQLite.DeleteCodingSession(log);
                                                    goto LogMenu;

                                                case "[red]Return to Log Menu[/]":
                                                    goto LogMenu;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Validation.LogActionError(e);
                                        }
                                    else
                                        goto ReportsMenu;
                                }
                                catch (Exception e)
                                {
                                    Validation.GetSessionsError(e);
                                }
                                break;

                            case "[red]Return to Main Menu[/]":
                                goto MainMenu;
                        }
                        break;

                    case "[red]Exit[/]":
                        return;
                }
            } while (true);
        }
    }
}
