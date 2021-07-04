using System.Diagnostics;

namespace ScrybeLogger
{
    public class ScrybeEventViewerLogger : ScrybeLogger
    {
        private EventLog EventLogger { get; set; }
        
        
        public ScrybeEventViewerLogger(decimal loggingLevel, string source)
            : base(loggingLevel)
        {
            EventLogger = new EventLog
            {
                Source = source
            };
        }


        public override void LogMessage(object message)
        {
            var entryType = GetEventLogEntryType();
            EventLogger.WriteEntry(message.ToString(), entryType);
        }


        private EventLogEntryType GetEventLogEntryType()
        {
            switch(CurrentLoggingLevel)
            {
                case "FORC":
                    return EventLogEntryType.Information;

                case "FATL":
                case "EROR":
                    return EventLogEntryType.Error;

                case "WARN":
                    return EventLogEntryType.Warning;

                case "INFO":
                case "DBUG":
                case "VERB":
                case "TRCE":
                default:
                    return EventLogEntryType.Information;
            }
        }
    }
}
