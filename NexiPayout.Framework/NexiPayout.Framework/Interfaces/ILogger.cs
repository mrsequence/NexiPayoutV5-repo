using Serilog;
using Serilog.Events;
using System;

namespace NexiPayout.Framework.Interfaces
{
    public interface ILogger
    {
        bool IsDebugEnabled { get; }
        bool IsInformationEnabled { get; }
        bool IsErrorEnabled { get; }
        void WriteInformation(string messageTemplate);
        void Write(LogEventLevel level, string messageTemplate);
        void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue);

        void Debug(Exception exception, string messageTemplate);
        void Debug<T>(Exception exception, string messageTemplate, T propertyValue);
        
        void Error(Exception exception, string messageTemplate);
        void Error<T>(Exception exception, string messageTemplate, T propertyValue);
    }
}
