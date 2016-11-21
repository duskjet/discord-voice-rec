using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public abstract class DiscordWebSocket : IDisposable
    {
        private ConcurrentQueue<ICommand> queue = new ConcurrentQueue<ICommand>();
        private WebSocket4Net.WebSocket websocket;
        private static Logger log = LogManager.GetCurrentClassLogger();
        private const int sendingInterval = 500;

        public DiscordWebSocket(string uri)
        {
            websocket = new WebSocket4Net.WebSocket(uri, null, WebSocket4Net.WebSocketVersion.Rfc6455);

            websocket.MessageReceived += Websocket_MessageReceived;
            websocket.Open();

            var sendloop = new Task(SendLoop, TaskCreationOptions.LongRunning); sendloop.Start();
        }

        public event EventHandler<WebSocket4Net.MessageReceivedEventArgs> Received;

        private void Websocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            log.Trace($"MessageReceived event: {e.Message}");
            Received?.Invoke(sender, e);
        }


        public async Task Send(ICommand command)
        {
            await Task.Run(() => queue.Enqueue(command)).ConfigureAwait(false);
        }

        private async void SendLoop()
        {
            while (true)
            {
                await Task.Delay(sendingInterval);

                if (queue.Count > 0)
                {
                    ICommand command; 
                    queue.TryDequeue(out command);
                    websocket.Send(command.Message);
                }
            }
        }

        public void Dispose()
        {
            websocket?.Dispose();
        }
    }
}
