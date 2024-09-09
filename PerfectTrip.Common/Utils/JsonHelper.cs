using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Utils
{
    public class JsonHelper
    {
        private readonly JsonSerializerSettings _settings;

        public JsonHelper(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }
    }
}
