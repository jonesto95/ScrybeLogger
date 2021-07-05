using System;

namespace ScrybeLogger.Interface
{
    public interface IScrybe<T>
    {
        bool IsTraceEnabled();

        void LogTrace(object message);

        bool IsVerboseEnabled();

        void LogVerbose(object message);

        bool IsDebugEnabled();

        void LogDebug(object message);

        bool IsInfoEnabled();

        void LogInfo(object message);

        bool IsWarnEnabled();

        void LogWarn(object message);

        bool IsErrorEnabled();

        void LogError(object message);

        void LogError(object message, Exception exception);

        bool IsFatalEnabled();

        void LogFatal(object message);

        bool IsForceEnabled();

        void LogForce(object message);
    }
}
