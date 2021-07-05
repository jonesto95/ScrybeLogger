using System.IO;

namespace ScrybeLogger
{
    public class ScrybeFileLogger : PrefixedScrybeLogger
    {
        private string FileName { get; set; }

        private string LoggingDirectory { get; set; }


        public ScrybeFileLogger(decimal loggingLevel, string logLinePrefix, string loggingDirectory, string fileName)
            : base(loggingLevel, logLinePrefix)
        {
            FileName = fileName;
            LoggingDirectory = Path.GetFullPath(loggingDirectory);
        }


        private string GetLoggingDirectory()
        {
            string loggingDirectory = ProcessMonikers(LoggingDirectory);
            Directory.CreateDirectory(loggingDirectory);
            return loggingDirectory;
        }


        private string GetFileName()
        {
            string fileName = ProcessMonikers(FileName);
            return fileName;
        }


        public override void LogMessage(object message)
        {
            string directory = GetLoggingDirectory();
            string fileName = GetFileName();
            string filePath = Path.Combine(directory, fileName);
            File.AppendAllLines(filePath, new string[] { message.ToString() });
        }
    }
}
