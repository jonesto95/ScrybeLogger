using System;

namespace ScrybeLogger
{
    internal class ScrybeLoggingLevel
    {
        public const decimal FORCE = decimal.MinValue;
        public const decimal FATAL = 2.0m;
        public const decimal ERROR = 4.0m;
        public const decimal WARN = 8.0m;
        public const decimal INFO = 16.0m;
        public const decimal DEBUG = 32.0m;
        public const decimal VERBOSE = 64.0m;
        public const decimal TRACE = 128.0m;
    }


    // TODO: Write header log upon log creation
    // TODO: Make loggers disposable in order to write footer log
    public abstract class ScrybeLogger
    {
        public decimal LoggingLevel { get; private set; }

        protected string CurrentLoggingLevel { get; set; }

        public ScrybeLogger(decimal loggingLevel)
        {
            LoggingLevel = loggingLevel;
            CurrentLoggingLevel = GetCurrentLoggingLevel(loggingLevel);
        }

        #region IsEnabled Properties

        /// <summary>
        /// Determine if the current logging level allows for TRACE logging.
        /// </summary>
        protected bool TraceEnabled => (LoggingLevel >= ScrybeLoggingLevel.TRACE);

        /// <summary>
        /// Determine if the current logging level allows for VERBOSE logging.
        /// </summary>
        protected bool VerboseEnabled => (LoggingLevel >= ScrybeLoggingLevel.VERBOSE);

        /// <summary>
        /// Determine if the current logging level allows for DEBUG logging.
        /// </summary>
        protected bool DebugEnabled => (LoggingLevel >= ScrybeLoggingLevel.DEBUG);

        /// <summary>
        /// Determine if the current logging level allows for INFO logging.
        /// </summary>
        protected bool InfoEnabled => (LoggingLevel >= ScrybeLoggingLevel.INFO);

        /// <summary>
        /// Determine if the current logging level allows for WARN logging.
        /// </summary>
        protected bool WarnEnabled => (LoggingLevel >= ScrybeLoggingLevel.WARN);

        /// <summary>
        /// Determine if the current logging level allows for ERROR logging.
        /// </summary>
        protected bool ErrorEnabled => (LoggingLevel >= ScrybeLoggingLevel.ERROR);

        /// <summary>
        /// Determine if the current logging level allows for FATAL logging.
        /// </summary>
        protected bool FatalEnabled => (LoggingLevel >= ScrybeLoggingLevel.FATAL);

        /// <summary>
        /// Determine if the current logging level allows for FORCE logging.
        /// This should always return true due to the nature of FORCE logging.
        /// </summary>
        protected bool ForceEnabled => true;

        #endregion


        #region Leveled Logging Methods

        /// <summary>
        /// Log a message at the TRACE level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogTrace(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.TRACE);
        }

        /// <summary>
        /// Log a message at the VERBOSE level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogVerbose(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.VERBOSE);
        }

        /// <summary>
        /// Log a message at the DEBUG level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogDebug(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.DEBUG);
        }

        /// <summary>
        /// Log a message at the INFO level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogInfo(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.INFO);
        }

        /// <summary>
        /// Log a message at the WARN level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogWarn(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.WARN);
        }

        /// <summary>
        /// Log a message at the ERROR level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogError(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.ERROR);
        }

        /// <summary>
        /// Log a message at the FATAL level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogFatal(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.FATAL);
        }

        /// <summary>
        /// Log a message at the FORCE level
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogForce(object message)
        {
            LogAtLevel(message, ScrybeLoggingLevel.FORCE);
        }

        /// <summary>
        /// Log a messsage at a custom logging level
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="loggingLevel"></param>
        public void LogAtLevel(object message, decimal loggingLevel)
        {
            if(LoggingLevel >= loggingLevel)
            {
                CurrentLoggingLevel = GetCurrentLoggingLevel(loggingLevel);
                message = ProcessMessage(message);
                LogMessage(message);
            }
        }


        public void LogMethodStart(params object[] parameters)
        {
            if(LoggingLevel >= ScrybeLoggingLevel.TRACE)
            {
                string callingMethod = GetCallingMethod();
                string message = $"Starting method {callingMethod}";
                LogTrace(message);
                if(parameters.Length > 0)
                {
                    string paramList = "Parameters:";
                    foreach(var param in parameters)
                    {
                        if(param == null)
                        {
                            paramList += $" null,";
                        }
                        else if(param is string paramString)
                        {
                            paramList += $" \"{paramString}\",";
                        }
                        else
                        {
                            paramList += $" {param},";
                        }
                    }
                    paramList = paramList.Substring(0, paramList.Length - 1);
                    LogTrace(paramList);
                }
            }
        }


        public void LogMethodEnd()
        {
            if(LoggingLevel >= ScrybeLoggingLevel.TRACE)
            {
                string callingMethod = GetCallingMethod();
                string message = $"Finishing method {callingMethod}";
                LogTrace(message);
            }
        }


        public void LogMethodEnd(object returnObject)
        {
            if (LoggingLevel >= ScrybeLoggingLevel.TRACE)
            {
                string callingMethod = GetCallingMethod();
                string message = $"Finishing method {callingMethod}, returning ";
                if(returnObject is string returnString)
                {
                    message += $"\"{returnString}\"";
                }
                else
                {
                    message += $"{returnObject}";
                }
                LogTrace(message);
            }
        }


        private string GetCallingMethod()
        {
            string[] stackTrace = Environment.StackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string methodToSearch = $"ScrybeLogger.ScrybeLogger.GetCallingMethod()";
            int i = 0;
            while(!stackTrace[i].Contains(methodToSearch))
            {
                i++;
            }
            string callMethod = stackTrace[i+3];
            int methodClose = callMethod.IndexOf(")");
            callMethod = callMethod.Substring(6, methodClose - 5);
            return callMethod;
        }

        #endregion


        #region Abstract Methods

        public abstract void LogMessage(object message);

        #endregion

        #region Virtual Methods

        public virtual object ProcessMessage(object message)
        {
            return message;
        }

        #endregion

        protected string ProcessMonikers(string input)
        {
            var now = DateTime.Now;
            input = input.Replace("{y}", now.Year.ToString());
            input = input.Replace("{M}", now.Month.ToString("00"));
            input = input.Replace("{d}", now.Day.ToString("00"));
            input = input.Replace("{H}", now.Hour.ToString("00"));
            input = input.Replace("{m}", now.Minute.ToString("00"));
            input = input.Replace("{s}", now.Second.ToString("00"));
            input = input.Replace("{f}", now.Millisecond.ToString("000"));
            input = input.Replace("{w}", ((int)now.DayOfWeek).ToString());
            input = input.Replace("{n}", CurrentLoggingLevel);
            input = input.Replace("{D}", now.ToString("yyyy-MM-dd"));
            input = input.Replace("{T}", now.ToString("HH.mm.ss"));
            input = input.Replace("{t}", now.ToString("HH.mm.ss.fff"));
            return input;
        }

        protected string GetCurrentLoggingLevel(decimal loggingLevel)
        {
            if(loggingLevel >= ScrybeLoggingLevel.TRACE)
            {
                return "TRCE";
            }
            if (loggingLevel >= ScrybeLoggingLevel.VERBOSE)
            {
                return "VERB";
            }
            if (loggingLevel >= ScrybeLoggingLevel.DEBUG)
            {
                return "DBUG";
            }
            if (loggingLevel >= ScrybeLoggingLevel.INFO)
            {
                return "INFO";
            }
            if (loggingLevel >= ScrybeLoggingLevel.WARN)
            {
                return "WARN";
            }
            if (loggingLevel >= ScrybeLoggingLevel.ERROR)
            {
                return "EROR";
            }
            if (loggingLevel >= ScrybeLoggingLevel.FATAL)
            {
                return "FATL";
            }
            if (loggingLevel >= ScrybeLoggingLevel.FORCE)
            {
                return "FORC";
            }
            return string.Empty;
        }
    }
}
