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
        public int OperationCode { get; set; }
        [JsonProperty("d", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { get; set; }
    }
}
