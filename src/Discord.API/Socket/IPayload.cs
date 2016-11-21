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

        public Payload(int opcode, T data)
        {
            if (typeof(T).GetTypeInfo().GetCustomAttribute(typeof(JsonObjectAttribute)) == null)
                throw new NotSupportedException("Data object must have a JsonObject attribute");

            OperationCode = opcode;
            Data = data;
        }
    }
}
