using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public class VoiceSocket : DiscordWebSocket
    {
        public VoiceSocket(string uri) : base(uri)
        {
        }
    }
}
