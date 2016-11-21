using Newtonsoft.Json;

namespace Discord.API.Socket.Events
{
    [JsonObject]
    public class Event<T>
    {
        [JsonProperty("op")]
        public int OperationCode { get; set; }
        [JsonProperty("d")]
        public T Data { get; set; }
    }
}
