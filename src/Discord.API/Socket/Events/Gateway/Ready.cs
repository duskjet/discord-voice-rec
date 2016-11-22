using Discord.API.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.API.Socket.Events.Gateway
{
    [JsonObject]
    public class Ready
    {
        [JsonProperty("v")]
        public int GatewayProtocolVersion { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("private_channels")]
        public List<DMChannel> PrivateChannels { get; set; }
        [JsonProperty("guilds")]
        public List<Guild> Guilds { get; set; }
        [JsonProperty("session_id")]
        public string SessionId { get; set; }
        //[JsonProperty("presences")]
        //[JsonProperty("relationships")]
        [JsonProperty("_trace")]
        public List<string> Trace { get; set; }
    }
}
