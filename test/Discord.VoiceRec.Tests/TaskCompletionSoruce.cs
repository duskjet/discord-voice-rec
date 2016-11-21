using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Discord.VoiceRec.Tests
{
    public class TaskCompletionSourceTest
    {
        [Fact]
        public async Task Test()
        {
            var tcs = new TaskCompletionSource<string>();
            var tcs2 = new TaskCompletionSource<int>();

            Task.Run(async () => { await Task.Delay(3000); tcs.SetResult("zhopa"); await Task.Delay(2000); tcs2.SetResult(20); } );
            
            Debug.WriteLine("Waiting for task to finish");

            var result = await ReadyPayload(tcs);
            Debug.WriteLine($"Result is {result}");
            Debug.WriteLine("Finish");
        }

        public async Task<string> ReadyPayload(TaskCompletionSource<string> tcs)
        {
            return tcs.Task.Result;
        }
    }
}
