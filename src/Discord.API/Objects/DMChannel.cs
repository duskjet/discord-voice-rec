using Newtonsoft.Json;

namespace Discord.API.Objects
{
    public class DMChannel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("recipient")]
        public User Recipient { get; set; }

        [JsonProperty("last_message_id")]
        public string LastMessageId { get; set; }
    }
}
