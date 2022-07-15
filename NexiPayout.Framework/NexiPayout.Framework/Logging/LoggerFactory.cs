using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void DestructLogger(ILogger logger)
        {
            Log.CloseAndFlush();
        }
    }
}
