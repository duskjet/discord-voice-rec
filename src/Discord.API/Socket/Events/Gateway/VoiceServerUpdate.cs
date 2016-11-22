using Newtonsoft.Json;

namespace Discord.API.Socket.Events.Gateway
{
    [JsonObject]
    public class VoiceServerUpdate
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
    }
}
