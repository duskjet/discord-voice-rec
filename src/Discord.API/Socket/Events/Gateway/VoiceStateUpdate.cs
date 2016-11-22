using Newtonsoft.Json;

namespace Discord.API.Socket.Events.Gateway
{
    [JsonObject]
    public class VoiceStateUpdate
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }
    }
}
