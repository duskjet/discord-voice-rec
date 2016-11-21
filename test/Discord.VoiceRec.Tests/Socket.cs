using Discord.API.Socket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Discord.VoiceRec.Tests
{
    public class SocketTests : IClassFixture<SocketTests.SocketFixture>
    {
        SocketFixture fixture;
        int possibleMessageOffset = 0;

        public SocketTests(SocketFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void SampleTest()
        {
            var kek = 123;
            Assert.Equal(123, kek);
        }

        [Fact]
        public async Task EnqueingMessages()
        {
            possibleMessageOffset++;
            await fixture.Websocket.Send(new Command("{\"sos\":\"kek\"}"));

            Assert.Equal(1, fixture.Websocket._Queue.Count);

            await Task.Delay(3000);
        }

        [Theory]
        [InlineData("{\"key\":\"value\"}")]
        [InlineData("pidor sosi zhopu")]
        public async Task SendMessage(string message)
        {
            EventHandler<WebSocket4Net.MessageReceivedEventArgs> handler = (s, e) =>
            {
                Debug.WriteLine(e.Message);
                var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject(e.Message);
                Assert.Equal(message, deserialized);
            };
            fixture.Websocket.Received += handler;

            possibleMessageOffset++;
            await fixture.Websocket.Send(new Command(message));
            await Task.Delay(3000);

            fixture.Websocket.Received -= handler;
        }

        /*
         * Test Cases:
         * socket connects
         * socket sends messages
         *   socket sends message
         *   socket sends multiple messages
         * message queue tests
         *   correctly adding msg to queue
         *   correctly dequeueing from queue
         *   correct behavior when queue empty
         * send limit tests
         *   limit behaves correctly when on low allowed operations
         *   correct transition when 0-1 operations left and new minute is coming
         * cancellation token source tests
         */

        public class SocketFixture : IDisposable
        {
            public MockSocket Websocket { get; private set; }

            public SocketFixture()
            {
                Websocket = new MockSocket("wss://echo.websocket.org");
            }

            public void Dispose()
            {
                Websocket.Dispose();
            }
        }

        public class MockSocket : DiscordWebSocket
        {
            public MockSocket(string uri) : base(uri)
            {
                while (base.State != WebSocket4Net.WebSocketState.Open) ;
            }
        }
    }
}
