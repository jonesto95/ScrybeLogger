using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScrybeLogger.Configuration;

namespace ScrybeLogger
{
    public static class ScrybeBuilder
    {
        public static Scrybe BuildScrybe<T>()
        {
            Scrybe result = new Scrybe();
            Type type = typeof(T);
            string scrybeTag = GetScrybeTagsFromType(type);
            if(!string.IsNullOrEmpty(scrybeTag))
            {
                result = BuildScrybeByTags(scrybeTag);
            }
            else
            {
                result = BuildScrybeByClassName(type.FullName);
            }
            return result;
        }


        private static Scrybe BuildScrybeByTags(string scrybeTag)
        {
            var result = new Scrybe();
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
            result.Loggers = loggerList.ToArray();
            return result;
        }


        private static Scrybe BuildScrybeByClassName(string className)
        {
            var result = new Scrybe();
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
            result.Loggers = loggerList.ToArray();
            return result;
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
