using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.API.Socket.Payloads.Gateway
{
    [JsonObject]
    public sealed class Identify
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("properties")]
        public IdentifyProperties Properties { get; set; }
        [JsonProperty("compress")]
        public bool Compress { get; set; }
        [JsonProperty("large_threshold")]
        public int LargeThreshold { get; set; }
        [JsonProperty("shard")]
        public List<int> Shard { get; set; }

        [JsonObject]
        public sealed class IdentifyProperties
        {
            [JsonProperty("$os")]
            public string OS { get; set; }
            [JsonProperty("$browser")]
            public string Browser { get; set; }
            [JsonProperty("$device")]
            public string Device { get; set; }
            [JsonProperty("$referrer")]
            public string Referrer { get; set; }
            [JsonProperty("$referring_domain")]
            public string ReferringDomain { get; set; }
        }
    }
}


