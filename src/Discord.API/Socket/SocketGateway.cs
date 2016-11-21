using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public class GatewaySocket : DiscordWebSocket
    {
        public GatewaySocket(string uri) : base(uri)
        {
        }
    }
}
