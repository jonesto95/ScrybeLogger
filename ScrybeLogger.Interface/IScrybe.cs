using System;

namespace ScrybeLogger.Interface
{
    public interface IScrybe<T>
    {
        void LogTrace(object message);

        void LogVerbose(object message);

        void LogDebug(object message);

        void LogInfo(object message);

        void LogWarn(object message);

        void LogError(object message);

        void LogFatal(object message);

        void LogForce(object message);

        void LogMethodStart(params object[] parameters);

        void LogMethodEnd();

        void LogMethodEnd(object returnObject);

        void LogErrorInMethod(Exception exception);
    }
}
