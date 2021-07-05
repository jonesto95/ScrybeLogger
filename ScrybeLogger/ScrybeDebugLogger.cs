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
    }
}
