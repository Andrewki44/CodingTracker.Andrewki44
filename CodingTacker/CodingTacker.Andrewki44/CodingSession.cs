using Spectre.Console;

namespace CodingTacker.Andrewki44
{
    class CodingSession
    {
        public DateTime? sessionStart { get; set; }
        public DateTime? sessionEnd { get; set; }
        public TimeSpan duration { get; set; }
        public double seconds
        {
            get { return duration.TotalSeconds; }
            set { duration = new TimeSpan(0, 0, (int)value); }
        }

        public CodingSession()
        {
        }

        public CodingSession(DateTime sessionStart)
        {
            this.sessionStart = sessionStart;
        }
        
        public CodingSession(DateTime sessionStart, DateTime sessionEnd)
        {
            this.sessionStart = sessionStart;
            this.sessionEnd = sessionEnd;
            this.duration = CalculateDuration();
        }

        public void SetSessionStart(DateTime sessionStart)
        {
            this.sessionStart = sessionStart;
            if (this.sessionEnd.HasValue)
                this.duration = CalculateDuration();
        }

        public void SetSessionEnd(DateTime sessionEnd)
        {
            this.sessionEnd = sessionEnd;
            if (this.sessionStart.HasValue)
                this.duration = CalculateDuration();
        }

        /// <summary>
        /// Start a live display of active session, until exited
        /// </summary>
        /// <returns></returns>
        public CodingSession StartSession()
        {
            this.sessionStart = DateTime.Now;
            
            //Setup table
            Table table = new Table()
                .Title("[red]~ Coding Session ~[/]")
                .Caption("[italic grey]Press Enter to stop the session...[/]")
                .AddColumn("Session Start")
                .AddColumn("Duration")
                .Width(40);

            //Display live duration of session, until complete
            AnsiConsole.Live(table).AutoClear(false).Start(ctx =>
            {
                table.AddRow(
                        new Text(this.sessionStart.Value.ToLongTimeString()),
                        new Text("")
                    );

                //Keep looping until Enter is pressed
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter))
                {
                    //Update duration cell with TimeSpan
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
        /// Calculate duration based on sessionStart & sessionEnd
        /// </summary>
        /// <returns></returns>
        private TimeSpan CalculateDuration()
        {
            if (this.sessionStart.HasValue && this.sessionEnd.HasValue)
                //Round to the second
                return TimeSpan.FromSeconds(Math.Round((this.sessionEnd.Value - this.sessionStart.Value).TotalSeconds));
            else
                return TimeSpan.Zero;
        }

        /// <summary>
        /// Calculate duration based on sessionStart & timeToCompare
        /// </summary>
        /// <param name="timeToCompare"></param>
        /// <returns></returns>
        private TimeSpan CalculateDuration(DateTime timeToCompare)
        {
            if (this.sessionStart.HasValue)
                return TimeSpan.FromSeconds(Math.Round((timeToCompare - this.sessionStart.Value).TotalSeconds));
            else
                return TimeSpan.Zero;
        }
    }
}


