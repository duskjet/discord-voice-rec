using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public class TaskSynchronization
    {
        private Dictionary<string, TaskCompletionSource<string>> TCS = new Dictionary<string, TaskCompletionSource<string>>();

        public TaskSynchronization(DiscordWebSocket socket)
        {
            socket.Received += MessageReceived;
        }

        public async Task<object> WaitFor(string @event, Type type) => await Synchronize(@event, type);

        public async Task<object> WaitFor(int opcode, Type type) => await Synchronize(opcode.ToString(), type);

        private async Task<object> Synchronize(string key, Type type)
        {
            var tcs = new TaskCompletionSource<string>();
            TCS.Add(key, tcs);

            var json = await tcs.Task;
            return JsonConvert.DeserializeObject(json, type);
        }

        private void MessageReceived(object sender, DiscordWebSocket.MessageReceivedEventArgs e)
        {
            var key = e.Payload.Event ?? e.Payload.Operation.ToString();

            if (TCS.ContainsKey(key)) TCS[key].SetResult(e.Payload.Data);
        }
    }
}
