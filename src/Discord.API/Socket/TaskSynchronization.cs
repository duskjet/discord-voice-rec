using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public class TaskSynchronization
    {
        private static Dictionary<int, TaskCompletionSource<dynamic>> TCS = new Dictionary<int, TaskCompletionSource<dynamic>>();

        public TaskSynchronization(DiscordWebSocket socket)
        {
            socket.Received += MessageReceived;
        }

        public void SetResponse(int opcode, int responsecode, string @event)
        {

        }

        private void MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            var obj = JsonConvert.DeserializeObject<Payload>(e.Message);
            
            TCS.Add(obj.OperationCode, obj)
        }
    }
}
