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
        public TaskSynchronization taskSync;

        private ConcurrentQueue<ICommand> queue = new ConcurrentQueue<ICommand>();
        private WebSocket4Net.WebSocket websocket;
        private static Logger log = LogManager.GetCurrentClassLogger();
        private const int sendingInterval = 500;

        public DiscordWebSocket(string uri)
        {
            websocket = new WebSocket4Net.WebSocket(uri, null, WebSocket4Net.WebSocketVersion.Rfc6455);
            taskSync = new TaskSynchronization(this);

            websocket.MessageReceived += Websocket_MessageReceived;
            websocket.Open();

            var sendloop = new Task(SendLoop, TaskCreationOptions.LongRunning); sendloop.Start();
        }

        public event EventHandler<MessageReceivedEventArgs> Received;

        public class MessageReceivedEventArgs : EventArgs
        {
            public MessageReceivedEventArgs(Payload payload)
            {
                Payload = payload;
            }

            public Payload Payload { get; set; } 
        }

        private void Websocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            log.Trace($"Message Received: {e.Message}");

            var payload = DeserializeMessage(e.Message);
            Received?.Invoke(sender, new MessageReceivedEventArgs(payload));
        }

        private Payload DeserializeMessage(string message) => JsonConvert.DeserializeObject<Payload>(message);

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

        private static Type GetTypeByEvent(string eventName) => eventTypes[eventName];
        private static Type GetTypeByOpcode(int opcode) => opcodeTypes[opcode];

        private static Dictionary<string, Type> eventTypes = new Dictionary<string, Type>()
        {
            ["READY"] = typeof(Events.Gateway.Ready),
            ["RESUMED"] = typeof(Events.Gateway.Resumed),
            ["VOICE_SERVER_UPDATE"] = typeof(Events.Gateway.VoiceServerUpdate),
            ["VOICE_STATE_UPDATE"] = typeof(Events.Gateway.VoiceStateUpdate)
        };

        private static Dictionary<int, Type> opcodeTypes = new Dictionary<int, Type>()
        {
            [(int)GatewayOperation.Heartbeat] = typeof(Payloads.Gateway.Heartbeat),
            [(int)GatewayOperation.Identify] = typeof(Payloads.Gateway.Identify),
            [(int)GatewayOperation.Resume] = typeof(Payloads.Gateway.Resume),
            [(int)GatewayOperation.VoiceStateUpdate] = typeof(Payloads.Gateway.VoiceStateUpdate),
        };
    }
}
