using Discord.API.Socket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Discord.VoiceRec.Tests
{
    public class RateLimitTests
    {
        [Fact]
        public async Task OperationDecrement()
        {
            var limit = new RateLimit(opm: 1);

            for(int i = 0; i < 1000; i++)
            {
                await limit.Wait(); // иногда усирается (в while попадает)
            }
        }
    }
}
