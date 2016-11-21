using Newtonsoft.Json;

namespace Discord.API.Socket.Payloads.Gateway
{
    public class Resume
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("seq")]
        public int Sequence { get; set; }
    }
}
