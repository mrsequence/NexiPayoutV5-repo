using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Utilities
{
    public class ConfigurationsUtility
    {
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
