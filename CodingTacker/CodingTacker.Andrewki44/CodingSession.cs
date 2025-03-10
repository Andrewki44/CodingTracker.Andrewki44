namespace CodingTacker.Andrewki44
{
    class CodingSession
    {
        public DateTime sessionStart { get; private set; }
        public DateTime? sessionEnd { get; private set; }
        public TimeSpan duration { get; private set; }

        public CodingSession()
        {
            this.sessionStart = DateTime.Now;
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
            this.duration = CalculateDuration();
        }
        
        private TimeSpan CalculateDuration()
        {
            if (this.sessionEnd.HasValue)
                //Round to the second
                return TimeSpan.FromSeconds(Math.Round((this.sessionEnd.Value - this.sessionStart).TotalSeconds));
            else
                return TimeSpan.Zero;
        }
    }
}


