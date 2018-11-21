using System.Collections.Generic;

namespace EscaperoomSoftware
{
    interface IJsonParser
    {
        Device ToDevice(string json);
        Dictionary<string, object> ToDict(string json);
        string toString(Device json);
    }
}