using System;
using ScrybeLogger.Interface;

namespace ScrybeLogger.Extensions
{
    public static class ScrybeLoggerExtensions
    {
        #region Call Stack Trace Methods

        public static void LogMethodStart<T>(this IScrybe<T> scrybe, params object[] parameters)
            where T : class
        {
            if(scrybe.IsTraceEnabled())
            {
                string callingMethod = GetCallingMethod();
                string message = $"Starting method {callingMethod}";
                scrybe.LogTrace(message);
                if (parameters.Length > 0)
                {
                    string paramList = "Parameters:";
                    foreach (var param in parameters)
                    {
                        if (param == null)
                        {
                            paramList += $" null,";
                        }
                        else if (param is string paramString)
                        {
                            paramList += $" \"{paramString}\",";
                        }
                        else
                        {
                            paramList += $" {param},";
                        }
                    }
                    paramList = paramList.Substring(0, paramList.Length - 1);
                    scrybe.LogDebug(paramList);
                }
            }
        }


        public static void LogMethodEnd<T>(this IScrybe<T> scrybe)
            where T : class
        {
            if(scrybe.IsTraceEnabled())
            {
                string callingMethod = GetCallingMethod();
                string message = $"Finishing method {callingMethod}";
                scrybe.LogTrace(message);
            }
        }


        public static T LogMethodEnd<T, U>(this IScrybe<U> scrybe, T returnObject)
            where U : class
        {
            try
            {
                if(scrybe.IsTraceEnabled())
                {
                    string callingMethod = GetCallingMethod();
                    string message = $"Finishing method {callingMethod}, returning ";
                    if (returnObject is string returnString)
                    {
                        message += $"\"{returnString}\"";
                    }
                    else
                    {
                        message += $"{returnObject}";
                    }
                    scrybe.LogTrace(message);
                }
            }
            catch(Exception e)
            {
                scrybe.LogErrorInMethod(e);
            }
            return returnObject;
        }


        public static void LogErrorInMethod<T>(this IScrybe<T> scrybe, Exception exception)
        {
            if(scrybe.IsErrorEnabled())
            {
                string callingMethod = GetCallingMethod();
                string message = $"Error thrown in {callingMethod}";
                scrybe.LogError(message, exception);
            }            
        }

        
        private static string GetCallingMethod()
        {
            string[] stackTrace = Environment.StackTrace.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string methodToSearch = $"ScrybeLogger.Extensions.ScrybeLoggerExtensions.GetCallingMethod()";
            int i = 0;
            while (!stackTrace[i].Contains(methodToSearch))
            {
                i++;
            }
            string callMethod = stackTrace[i + 2];
            int methodClose = callMethod.IndexOf(")");
            callMethod = callMethod.Substring(6, methodClose - 5);
            return callMethod;
        }

        #endregion


        public static void LogVariableValue<T>(this IScrybe<T> scrybe, string variableName, object variableValue)
        {
            if(scrybe.IsDebugEnabled())
            {
                string message = $"{variableName}: {variableValue}";
                scrybe.LogDebug(message);
            }
        }
    }
}
