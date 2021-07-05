using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScrybeLogger.Configuration;

namespace ScrybeLogger
{
    public static class ScrybeBuilder
    {
        public static ScrybeLogger[] GetScrybeLoggers<T>()
        {
            Type type = typeof(T);
            string scrybeTag = GetScrybeTagsFromType(type);
            var loggers = new ScrybeLogger[0];
            if(!string.IsNullOrEmpty(scrybeTag))
            {
                loggers = GetScrybeLoggersByTags(scrybeTag);
            }
            else
            {
                loggers = GetScrybeLoggersByClassName(type.FullName);
            }
            return loggers;
        }


        private static ScrybeLogger[] GetScrybeLoggersByTags(string scrybeTag)
        {
            List<ScrybeLogger>  loggerList = new List<ScrybeLogger>();
            string[] scrybeTags = scrybeTag.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var configuredTags = ScrybeLoggerConfiguration.GetTagFilters();
            foreach(string tag in scrybeTags)
            {
                if(configuredTags.ContainsKey(tag))
                {
                    var loggerConfig = (Dictionary<object, object>)configuredTags[tag];
                    var loggers = ScrybeLoggerFactory.BuildScrybeLoggerCollection(loggerConfig);
                    loggerList.AddRange(loggers.Item1);
                    if(loggers.Item2)
                    {
                        break;
                    }
                }
            }
            return loggerList.ToArray();
        }


        private static ScrybeLogger[] GetScrybeLoggersByClassName(string className)
        {
            List<ScrybeLogger> loggerList = new List<ScrybeLogger>();
            className = className.ToUpper();
            var configuredClasses = ScrybeLoggerConfiguration.GetClassFilters();
            foreach(var key in configuredClasses.Keys)
            {
                string keyRegex = key.ToString().ToUpper();
                keyRegex = keyRegex.Replace(".", "[.]");
                keyRegex = keyRegex.Replace("*", ".*");
                var regex = new Regex(keyRegex);
                if(regex.IsMatch(className))
                {
                    var loggerConfig = (Dictionary<object, object>)configuredClasses[key];
                    var loggers = ScrybeLoggerFactory.BuildScrybeLoggerCollection(loggerConfig);
                    loggerList.AddRange(loggers.Item1);
                    if (loggers.Item2)
                    {
                        break;
                    }
                }
            }
            return loggerList.ToArray();
        }


        private static string GetScrybeTagsFromType(Type type)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            foreach (var attribute in attributes)
            {
                if (attribute is ScrybeAttribute scrybeAttribute)
                {
                    return scrybeAttribute.Tags;
                }
            }
            return string.Empty;
        }
    }
}
