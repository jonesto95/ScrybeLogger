using System;
using System.Collections.Generic;

namespace ScrybeLogger
{
    internal static class ScrybeLoggerFactory
    {
        internal static (ScrybeLogger[], bool) BuildScrybeLoggerCollection(Dictionary<object, object> configuration)
        {
            decimal defaultLoggingLevel = ScrybeLoggingLevel.ERROR;
            List<ScrybeLogger> loggerList = new List<ScrybeLogger>();
            bool stopScan = false;
            if(configuration.ContainsKey("LoggingLevel"))
            {
                string loggingLevel = configuration["LoggingLevel"].ToString();
                defaultLoggingLevel = GetLoggingLevel(loggingLevel);
            }
            if(configuration.ContainsKey("StopScan"))
            {
                string value = configuration["StopScan"].ToString();
                stopScan = (string.Equals(value, "true", StringComparison.InvariantCultureIgnoreCase));
            }

            var loggers = (List<object>)configuration["Loggers"];
            foreach(var logger in loggers)
            {
                var resultLogger = BuildLogger((Dictionary<object, object>)logger, defaultLoggingLevel);
                if(resultLogger != null)
                {
                    loggerList.Add(resultLogger);
                }
            }

            return (loggerList.ToArray(), stopScan);
        }


        private static ScrybeLogger BuildLogger(Dictionary<object, object> loggerConfig, decimal defaultLoggingLevel)
        {
            ScrybeLogger result = null;
            if (loggerConfig.ContainsKey("Disabled"))
            {
                string disabledString = loggerConfig["Disabled"].ToString();
                if(string.Equals(disabledString, "true", StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }
            }
            decimal loggingLevel = defaultLoggingLevel;
            if(loggerConfig.ContainsKey("LoggingLevel"))
            {
                string loggingLevelString = loggerConfig["LoggingLevel"].ToString();
                loggingLevel = GetLoggingLevel(loggingLevelString);
            }
            string loggerType = loggerConfig["LoggerType"].ToString();
            switch(loggerType.ToUpper())
            {
                case "SCRYBECONSOLELOGGER":
                case "CONSOLELOGGER":
                case "CONSOLE":
                    string consolelogLinePrefix = string.Empty;
                    if(loggerConfig.ContainsKey("LogLinePrefix"))
                    {
                        consolelogLinePrefix = loggerConfig["LogLinePrefix"].ToString();
                    }
                    result = new ScrybeConsoleLogger(loggingLevel, consolelogLinePrefix);
                    return result;

                case "SCRYBEDEBUGLOGGER":
                case "DEBUGLOGGER":
                case "DEBUG":
                    string debugLogLinePrefix = string.Empty;
                    if (loggerConfig.ContainsKey("LogLinePrefix"))
                    {
                        debugLogLinePrefix = loggerConfig["LogLinePrefix"].ToString();
                    }
                    result = new ScrybeDebugLogger(loggingLevel, debugLogLinePrefix);
                    return result;

                case "SCRYBEERRORLOGGER":
                case "ERRORLOGGER":
                case "ERROR":
                    string errorlogLinePrefix = string.Empty;
                    if (loggerConfig.ContainsKey("LogLinePrefix"))
                    {
                        errorlogLinePrefix = loggerConfig["LogLinePrefix"].ToString();
                    }
                    result = new ScrybeErrorLogger(loggingLevel, errorlogLinePrefix);
                    return result;

                case "SCRYBEFILELOGGER":
                case "FILELOGGER":
                case "FILE":
                    string fileLogLinePrefix = string.Empty;
                    if (loggerConfig.ContainsKey("LogLinePrefix"))
                    {
                        fileLogLinePrefix = loggerConfig["LogLinePrefix"].ToString();
                    }
                    string loggingDirectory = loggerConfig["LogDirectory"].ToString();
                    string fileName = loggerConfig["FileName"].ToString();
                    result = new ScrybeFileLogger(loggingLevel, fileLogLinePrefix, loggingDirectory, fileName);
                    break;

                case "SCRYBEMSSQLLOGGER":
                case "MSSQLLOGGER":
                case "MSSQL":
                    string tableName = "ScrybeLog";
                    if (loggerConfig.ContainsKey("TableName"))
                    {
                        tableName = loggerConfig["TableName"].ToString();
                    }
                    string connectionString = loggerConfig["ConnectionString"].ToString();
                    result = new ScrybeMSSQLLogger(loggingLevel, connectionString, tableName);
                    break;

                case "SCRYBEEVENTVIEWERLOGGER":
                case "EVENTVIEWERLOGGER":
                case "EVENTVIEWER":
                    string source = "Application";
                    if(loggerConfig.ContainsKey("Source"))
                    {
                        source = loggerConfig["Source"].ToString();
                    }
                    result = new ScrybeEventViewerLogger(loggingLevel, source);
                    break;

                // TODO: Build custom logger
                case "SCRYBECUSTOMLOGGER":
                case "CUSTOMLOGGER":
                case "CUSTOM":
                    break;
                
                default:
                    break;
            }
            return result;
        }


        private static decimal GetLoggingLevel(string loggingLevel)
        {
            loggingLevel = loggingLevel.ToUpper();
            switch(loggingLevel)
            {
                case "ALL":
                    return decimal.MaxValue;

                case nameof(ScrybeLoggingLevel.TRACE):
                    return ScrybeLoggingLevel.TRACE;

                case nameof(ScrybeLoggingLevel.VERBOSE):
                    return ScrybeLoggingLevel.VERBOSE;

                case nameof(ScrybeLoggingLevel.DEBUG):
                    return ScrybeLoggingLevel.DEBUG;

                case nameof(ScrybeLoggingLevel.INFO):
                    return ScrybeLoggingLevel.INFO;

                case nameof(ScrybeLoggingLevel.WARN):
                    return ScrybeLoggingLevel.WARN;

                case nameof(ScrybeLoggingLevel.ERROR):
                    return ScrybeLoggingLevel.ERROR;

                case nameof(ScrybeLoggingLevel.FATAL):
                    return ScrybeLoggingLevel.FATAL;

                case nameof(ScrybeLoggingLevel.FORCE):
                    return ScrybeLoggingLevel.FORCE;

                default:
                    return decimal.Parse(loggingLevel);
            }
        }
    }
}
