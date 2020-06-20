using System;

public static class DebugLogger 
{
    public static LogSeverity MinimumSeverity = LogSeverity.Debug;

    public static void Log(string message, LogSeverity severity)
    {
        message = String.Format("{0} - {1}", DateTime.Now.ToString("hh:mm:ss.fff"), message);

        if (severity >= MinimumSeverity)
        {
            switch (severity)
            {
                case LogSeverity.Debug:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogSeverity.Info:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogSeverity.Warning:
                    UnityEngine.Debug.LogWarning(message);
                    break;
                case LogSeverity.Error:
                    UnityEngine.Debug.LogError(message);
                    break;
                case LogSeverity.Critical:
                    UnityEngine.Debug.LogError(message);
                    break;

                default:
                    UnityEngine.Debug.Log(message);
                    break;
            }
        }
    }
}

public enum LogSeverity
{
    Debug = 0,
    Info = 1,
    Warning = 2,
    Error = 3,
    Critical = 4,
}

