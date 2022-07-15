using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Utilities
{
    public class ApiUtility
    {
        public static string ConstructEndPointParams(string endpoint, Dictionary<string, string> parameters)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                if (i == 0)
                {
                    endpoint += "?";
                }

                string key = parameters.Keys.ToList()[i];
                string keyPairValue = key + "=" + parameters[key];
                if (parameters.Count == 1)
                {
                    endpoint += keyPairValue;
                    break;
                }

                if (i >= 0 && i < parameters.Count - 1)
                {
                    endpoint += keyPairValue + ";";
                }
                else
                {
                    endpoint += keyPairValue;
                }
            }
            return endpoint;
        }
    }
}
