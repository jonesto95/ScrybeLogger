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
    }
}
