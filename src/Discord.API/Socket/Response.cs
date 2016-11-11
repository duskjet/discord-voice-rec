using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public abstract class Response<TEnum> 
    {
        public TEnum OperationCode { get; private set; }
        public dynamic Data { get; private set; }

        public string Event { get; set; }
        public int Sequence { get; set; }

        public Response(TEnum opcode, dynamic data)
        {
            OperationCode = opcode;
            Data = data;
        }
    }
}
