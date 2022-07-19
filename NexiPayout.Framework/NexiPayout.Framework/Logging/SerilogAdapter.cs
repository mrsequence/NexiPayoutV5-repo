using Serilog.Events;
using System;

namespace NexiPayout.Framework.Logging
{
    public class SerilogAdapter : Interfaces.ILogger
    {
        public bool IsDebugEnabled => LoggerFactory.InitilizeLogger().IsEnabled(LogEventLevel.Debug);
        public bool IsInformationEnabled => LoggerFactory.InitilizeLogger().IsEnabled(LogEventLevel.Information);
        public bool IsErrorEnabled => LoggerFactory.InitilizeLogger().IsEnabled(LogEventLevel.Error);

        public void Debug(Exception exception, string messageTemplate)
        {
            LoggerFactory.InitilizeLogger().Debug(exception, messageTemplate);
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            LoggerFactory.InitilizeLogger().Debug(exception, messageTemplate, propertyValue);
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
            LoggerFactory.InitilizeLogger().Write(level, messageTemplate);
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            LoggerFactory.InitilizeLogger().Write(level, messageTemplate, propertyValue);
        }

        public void WriteInformation(string messageTemplate)
        {
            LoggerFactory.InitilizeLogger().Write(LogEventLevel.Information, messageTemplate);
        }

        public void Error(Exception exception, string messageTemplate)
        {
            LoggerFactory.InitilizeLogger().Error(exception, messageTemplate);
        }

        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            LoggerFactory.InitilizeLogger().Error(exception, messageTemplate, propertyValue);
        }
    }
}
