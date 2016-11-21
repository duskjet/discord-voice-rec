using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.API.Socket.Events.Gateway
{
    [JsonObject]
    public class Resumed
    {
        [JsonProperty("_trace")]
        public List<string> Trace { get; set; }
    }
}
