using System;
using System.Collections.Generic;

namespace ScrybeLogger.Configuration
{
    public static class ScrybeLoggerConfiguration
    {
        private const string DefaultConfigFilePath = "./ScrybeLoggerConfig.yaml";

        internal static YamlReader LoggerConfiguration
        {
            get
            {
                if(loggerConfiguration == null)
                {
                    loggerConfiguration = new YamlReader(DefaultConfigFilePath);
                }
                return loggerConfiguration;
            }
            set
            {
                loggerConfiguration = value;
            }
        }
        private static YamlReader loggerConfiguration;


        public static Dictionary<object, object> GetTagFilters()
        {
            return LoggerConfiguration.GetValues("ScrybeLogger.TagFilters");
        }


        public static Dictionary<object, object> GetClassFilters()
        {
            return LoggerConfiguration.GetValues("ScrybeLogger.ClassFilters");
        }
    }
}
