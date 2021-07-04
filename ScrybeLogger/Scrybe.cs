﻿using System;

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


    public class Scrybe
    {
        internal Scrybe() { }

        internal ScrybeLogger[] Loggers { get; set; }

        public void LogTrace(object message)
        {
            foreach(var logger in Loggers)
            {
                logger.LogTrace(message);
            }
        }

        public void LogVerbose(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogVerbose(message);
            }
        }

        public void LogDebug(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogDebug(message);
            }
        }

        public void LogInfo(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogInfo(message);
            }
        }

        public void LogWarn(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogWarn(message);
            }
        }

        public void LogError(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogError(message);
            }
        }

        public void LogFatal(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogFatal(message);
            }
        }

        public void LogForce(object message)
        {
            foreach (var logger in Loggers)
            {
                logger.LogForce(message);
            }
        }
    }
}