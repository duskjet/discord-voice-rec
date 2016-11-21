using Discord.API.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket.Events.Gateway
{
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
