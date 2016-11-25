using Discord.API.Socket.Payloads.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    [JsonObject]
    public class Payload<T> where T : class
    {
        [JsonProperty("op"), JsonRequired]
        public int OperationCode { get; }
        [JsonProperty("d", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; }

        public Payload(T data)
        {
            //if (typeof(T).GetTypeInfo().GetCustomAttribute(typeof(JsonObjectAttribute)) == null)
            //    throw new NotSupportedException("Data object must have a JsonObject attribute");

            OperationCode = operations[typeof(T)];
            Data = data;
        }

        // TODO: finish
        private static readonly Dictionary<Type, int> operations = new Dictionary<Type, int>()
        {
            [typeof(Heartbeat)] = 1,
            [typeof(Identify)] = 2
        };
    }

    [JsonObject]
    public class Payload
    {
        [JsonProperty("op"), JsonRequired]
        public int Operation { get; set; }
        [JsonProperty("d", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }
        [JsonProperty("t", NullValueHandling = NullValueHandling.Ignore)]
        public string Event { get; set; }
        [JsonProperty("s", NullValueHandling = NullValueHandling.Ignore)]
        public int Sequence { get; set; }
    }
}
