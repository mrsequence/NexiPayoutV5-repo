using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiPayout.Helpers
{
    public class ClientHelper <T>
    {
        public string SerializeContent(T content)
        {
            return JsonConvert.SerializeObject(content, Formatting.Indented);
        }

        public T DeserializeContent(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
