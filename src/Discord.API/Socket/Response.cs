using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    [JsonObject]
    public class Response<T> : Payload<T> where T : class
    {
        public Response(T data) : base(data) { }

        [JsonProperty("t")]
        public string Event { get; set; }
        [JsonProperty("s")]
        public int Sequence { get; set; }
    }
}
