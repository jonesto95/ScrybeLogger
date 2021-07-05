namespace ScrybeLogger
{
    public abstract class PrefixedScrybeLogger : ScrybeLogger
    {
        protected string LogLinePrefix { get; private set; }

        public PrefixedScrybeLogger(decimal loggingLevel, string logLinePrefix) 
            : base(loggingLevel)
        {
            LogLinePrefix = logLinePrefix;
        }


        public override object ProcessMessage(object message)
        {
            return AddPrefixToMessage(message);
        }


        public string AddPrefixToMessage(object message)
        {
            string messageString = $"{message}";
            string prefix = ProcessMonikers(LogLinePrefix);
            if(prefix.Length > 0)
            {
                messageString = $"{prefix} {message}";
            }
            return messageString;
        }
    }
}
