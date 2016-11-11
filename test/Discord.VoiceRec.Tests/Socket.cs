using Discord.API.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Discord.VoiceRec.Tests
{
    public class SocketTests
    {
        [Fact]
        public void SampleTest()
        {
            var kek = 123;
            Assert.Equal(123, kek);
        }

        [Fact]
        public async Task RateLimit_InitiallyReady()
        {
            var limit = new RateLimit();

            Assert.Equal(true, limit.Ready);
        }

        [Fact]
        public async Task RateLimit_NotReadyAfterFire()
        {
            var limit = new RateLimit(500);

            limit.Fire();
            Task.Run(() => limit.Fire());
            Task.Run(() => limit.Fire());


            Assert.Equal(false, limit.Ready);

            await Task.Delay(3500);

            Assert.Equal(true, limit.Ready);
        }
    }
}
