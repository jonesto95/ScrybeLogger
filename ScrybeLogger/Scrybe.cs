using System;
using ScrybeLogger.Interface;

namespace ScrybeLogger
{
    public class ScrybeAttribute : Attribute
    {
        public string Tags { get; set; }

        public ScrybeAttribute(string tags)
        {
            Tags = tags;
        }
    }


    public class Scrybe<T> : IScrybe<T>
    {
        public Scrybe() { }

        internal readonly ScrybeLogger[] Loggers = ScrybeBuilder.GetScrybeLoggers<T>();

        public bool IsTraceEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.TRACE);
        }

        public void LogTrace(object message)
        {
            foreach(var logger in Loggers)
            {
                logger.LogTrace(message);
            }
        }
        public bool IsVerboseEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.VERBOSE);
        }

        public void LogVerbose(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogVerbose(message);
            }
        }
        public bool IsDebugEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.DEBUG);
        }

        public void LogDebug(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogDebug(message);
            }
        }

        public bool IsInfoEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.INFO);
        }

        public void LogInfo(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogInfo(message);
            }
        }

        public bool IsWarnEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.WARN);
        }

        public void LogWarn(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogWarn(message);
            }
        }

        public bool IsErrorEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.ERROR);
        }

        public void LogError(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogError(message);
            }
        }

        public void LogError(object message, Exception exception)
        {
            foreach (var logger in Loggers)
            {
                logger.LogError(message, exception);
            }
        }

        public bool IsFatalEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.FATAL);
        }

        public void LogFatal(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogFatal(message);
            }
        }

        public bool IsForceEnabled()
        {
            return IsLoggingLevelEnabled(ScrybeLoggingLevel.FORCE);
        }

        public void LogForce(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogForce(message);
            }
        }

        private bool IsLoggingLevelEnabled(decimal loggingLevel)
        {
            foreach (var logger in Loggers)
            {
                if (logger.LoggingLevel >= loggingLevel)
                    return true;
            }
            return false;
        }
    }
}
