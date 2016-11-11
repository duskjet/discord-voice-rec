using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    //interface IPayload<TEnum>
    //{
    //    TEnum OperationCode { get; }
    //    dynamic Data { get; }
    //}

    [JsonObject]
    public class Payload<TEnum, TData> where TEnum : struct
    {
        [JsonProperty("op"), JsonRequired]
        public TEnum OperationCode { get; private set; }
        [JsonProperty("d"), JsonRequired]
        public TData Data { get; private set; }

        public Payload(TEnum opcode, TData data)
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
                throw new NotSupportedException("Operation code value must be Enum");

            if (typeof(TData).GetTypeInfo().GetCustomAttribute(typeof(JsonObjectAttribute)) == null)
                throw new NotSupportedException("Data object must have a JsonObject attribute");

            OperationCode = opcode;
            Data = data;
        }
    }

    public interface IJsonSerializable
    {
    }
}
