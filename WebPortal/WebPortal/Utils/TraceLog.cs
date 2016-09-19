using System;
using System.Diagnostics;

namespace WebPortal.Utils
{
    public class TraceLog
    {
        private EventLog log;

        /* Singleton */
        private static readonly Lazy<TraceLog> lazy = new Lazy<TraceLog>(() => new TraceLog());
        public static TraceLog Instance { get { return lazy.Value; } }

        private TraceLog()
        {
            this.log        = new EventLog();
            this.log.Source = "WebPortal";
            this.log.Log    = "Application";

#if !DEBUG
            this.log.BeginInit();
            if (!EventLog.SourceExists(this.log.Source))
            {
                EventLog.CreateEventSource(this.log.Source, this.log.Log);
            }
            this.log.EndInit();
#endif
        }

        public void LogInfo(string info)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("INFO: " + info);
#else
            this.log.WriteEntry(info, EventLogEntryType.Information);
#endif
        }

        public void LogError(string error)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("ERROR: " + error);
#else
            this.log.WriteEntry(error, EventLogEntryType.Error);
#endif
        }
    }
}
