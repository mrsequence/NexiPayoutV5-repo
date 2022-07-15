using NexiPayout.Framework.Interfaces;
using Serilog;
using Serilog.Events;
using System;

namespace NexiPayout.Framework.Logging
{
    public class SerilogAdapter : Interfaces.ILogger
    {
        public bool IsDebugEnabled => Log.IsEnabled(LogEventLevel.Debug);
        public bool IsInformationEnabled => Log.IsEnabled(LogEventLevel.Information);

        public void Debug(Exception exception, string messageTemplate)
        {
            Debug(exception, messageTemplate);
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Debug(exception, messageTemplate, propertyValue);
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
            Write(level, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            Write(level, messageTemplate, propertyValue);
        }
    }
}
