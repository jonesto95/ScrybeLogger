using System.Diagnostics;

namespace ScrybeLogger
{
    public class ScrybeDebugLogger : PrefixedScrybeLogger
    {
        public ScrybeDebugLogger(decimal loggingLevel, string logLinePrefix) : base(loggingLevel, logLinePrefix) { }


        public override void LogMessage(object message)
        {
            Debug.WriteLine(message);
        }


        public override object ProcessMessage(object message)
        {
            string prefix = ProcessMonikers(LogLinePrefix);
            string messageString = $"{prefix} {message}";
            return messageString;
        }
    }
}
