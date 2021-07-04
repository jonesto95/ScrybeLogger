using System;

namespace ScrybeLogger
{
    // TODO: Add color to console
    public class ScrybeConsoleLogger : PrefixedScrybeLogger
    {
        public ScrybeConsoleLogger(decimal loggingLevel, string logLinePrefix) : base(loggingLevel, logLinePrefix) { }


        public override void LogMessage(object message)
        {
            Console.WriteLine(message);
        }


        public override object ProcessMessage(object message)
        {
            string prefix = ProcessMonikers(LogLinePrefix);
            string messageString = $"{prefix} {message}";
            return messageString;
        }
    }
}
