using System;
using System.IO;

namespace ScrybeLogger
{
    public class ScrybeErrorLogger : PrefixedScrybeLogger
    {
        private readonly TextWriter ErrorWriter = Console.Error;


        public ScrybeErrorLogger(decimal loggingLevel, string logLinePrefix) : base(loggingLevel, logLinePrefix) { }


        public override void LogMessage(object message)
        {
            ErrorWriter.WriteLine(message);
        }
    }
}
