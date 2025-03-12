using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    class CodingSession
    {
        public int ID { get; set; }
        public DateTime? SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
        public TimeSpan Duration { get; set; }
        public double Seconds
        {
            get { return Duration.TotalSeconds; }
            set { Duration = new TimeSpan(0, 0, (int)value); }
        }

        public CodingSession()
        {
        }

        public CodingSession(DateTime sessionStart)
        {
            this.SessionStart = sessionStart;
        }
        
        public CodingSession(DateTime sessionStart, DateTime sessionEnd)
        {
            this.SessionStart = sessionStart;
            this.SessionEnd = sessionEnd;
            this.Duration = CalculateDuration();
        }

        public void SetSessionStart(DateTime sessionStart)
        {
            this.SessionStart = sessionStart;
            if (this.SessionEnd.HasValue)
                this.Duration = CalculateDuration();
        }

        public void SetSessionEnd(DateTime sessionEnd)
        {
            this.SessionEnd = sessionEnd;
            if (this.SessionStart.HasValue)
                this.Duration = CalculateDuration();
        }

        /// <summary>
        /// Start a live display of active session, until exited
        /// </summary>
        /// <returns></returns>
        public CodingSession StartSession()
        {
            this.SessionStart = DateTime.Now;
            
            //Setup table
            Table table = new Table()
                .Title("[red]~ Coding Session ~[/]")
                .Caption("[italic grey]Press Enter to stop the session...[/]")
                .AddColumn("Session Start")
                .AddColumn("Duration")
                .Width(40);

            //Display live Duration of session, until complete
            AnsiConsole.Live(table).AutoClear(false).Start(ctx =>
            {
                table.AddRow(
                        new Text(this.SessionStart.Value.ToLongTimeString()),
                        new Text("")
                    );

                //Keep looping until Enter is pressed
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter))
                {
                    //Update Duration cell with TimeSpan
                    table.UpdateCell(0, 1, CalculateDuration(DateTime.Now).ToString());                    
                    ctx.Refresh();
                }
            });

            //End and calculate session
            this.SetSessionEnd(DateTime.Now);
            return this;            
        }

        /// <summary>
        /// Gather a manual session
        /// </summary>
        /// <returns></returns>
        public CodingSession ManualSession()
        {
            (DateTime, DateTime) sessionTimes = Menu.ManualSessionMenu();
            SetSessionStart(sessionTimes.Item1);
            SetSessionEnd(sessionTimes.Item2);
            
            return this;
        }
        
        /// <summary>
        /// Calculate Duration based on SessionStart & SessionEnd
        /// </summary>
        /// <returns></returns>
        private TimeSpan CalculateDuration()
        {
            if (this.SessionStart.HasValue && this.SessionEnd.HasValue)
                //Round to the second
                return TimeSpan.FromSeconds(Math.Round((this.SessionEnd.Value - this.SessionStart.Value).TotalSeconds));
            else
                return TimeSpan.Zero;
        }

        /// <summary>
        /// Calculate Duration based on SessionStart & timeToCompare
        /// </summary>
        /// <param name="timeToCompare"></param>
        /// <returns></returns>
        private TimeSpan CalculateDuration(DateTime timeToCompare)
        {
            if (this.SessionStart.HasValue)
                return TimeSpan.FromSeconds(Math.Round((timeToCompare - this.SessionStart.Value).TotalSeconds));
            else
                return TimeSpan.Zero;
        }
    }
}


