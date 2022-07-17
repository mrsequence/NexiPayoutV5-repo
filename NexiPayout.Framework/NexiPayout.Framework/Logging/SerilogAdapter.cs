using Serilog;
using Serilog.Events;
using System;

namespace NexiPayout.Framework.Logging
{
    public class SerilogAdapter : Interfaces.ILogger
    {
        public bool IsDebugEnabled => Log.IsEnabled(LogEventLevel.Debug);
        public bool IsInformationEnabled => Log.IsEnabled(LogEventLevel.Information);
        public bool IsErrorEnabled => Log.IsEnabled(LogEventLevel.Error);

        public void Debug(Exception exception, string messageTemplate)
        {
            Log.Debug(exception, messageTemplate);
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Debug(exception, messageTemplate, propertyValue);
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
            Log.Write(level, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            Log.Write(level, messageTemplate, propertyValue);
        }

        public void WriteInformation(string messageTemplate)
        {
            Log.Write(LogEventLevel.Information, messageTemplate);
        }

        public void Error(Exception exception, string messageTemplate)
        {
            Log.Error(exception, messageTemplate);
        }

        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Log.Error(exception, messageTemplate, propertyValue);
        }
    }
}
