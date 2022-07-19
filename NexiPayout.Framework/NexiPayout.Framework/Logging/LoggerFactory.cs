using Serilog;

namespace NexiPayout.Framework.Logging
{
    public class LoggerFactory
    {
        public static ILogger InitilizeLogger()
        {
           return new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();
        }
        public static void DestructLogger()
        {
            Log.CloseAndFlush();
        }
    }
}
