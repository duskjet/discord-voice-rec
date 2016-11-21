using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public class Command : ICommand
    {
        public Command(object payload)
        {
            Message = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        }

        public string Message { get; set; }
        public bool Priority { get; set; }
    }
}
