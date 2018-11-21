using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EscaperoomSoftware
{
    class JsonParser : IJsonParser
    {
        public Device ToDevice(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Device>(json);
            }
            catch (JsonReaderException e)
            {
                throw new ArgumentException("The string does not contain correct Json.", e);
            }
        }

        public string toString(Device json)
        {
            return JsonConvert.SerializeObject(json);
        }

        public Dictionary<string, object> ToDict(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {
                throw new ArgumentException("The string does not contain correct Json.", e);
            }
        }
    }
}
