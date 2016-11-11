using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public abstract class DiscordWebSocket
    {
        private Queue<string> queue = new Queue<string>();
        private readonly object sync = new object();

        private const int sendingInterval = 500;

        public async Task Send(object message)
        {
            var json = JsonConvert.SerializeObject(message);

            await Task.Run(() => Enqueue(json));
        }

        private async Task SendLoop()
        {
            while (true)
            {
                await Task.Delay(sendingInterval);

            }
        }

        private void Enqueue(string message) { lock (sync) queue.Enqueue(message); }

        private object Dequeue() { lock (sync) return queue.Dequeue(); }
    }
}
