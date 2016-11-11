using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using debug = System.Diagnostics.Debug;
namespace Discord.API.Socket
{
    public class RateLimit
    {
        public RateLimit(int interval = 500)
        {
            this.interval = interval;
            cooldownTask = new Task(async () =>
            {
                await Task.Delay(interval);
                Ready = true;
                debug.WriteLine("Cooldown end");
            });
        }
        private Task cooldownTask;
        private Task currentAction;
        private int interval;

        public bool Ready { get; private set; } = true;

        public async Task Fire(Action action)
        {
            Task.Factory.StartNew(action);
        }

        private async Task Cooldown() // классный даун
        {
            debug.WriteLine("Starting cooldown");
            if (CooldownReady())
            {
                debug.WriteLine("Entered task run");
                cooldownTask.Start();
            }
            else
            {
                debug.WriteLine("Not ready");
            }
        }

        private async Task WaitUntilReady()
        {
            while (!Ready && !ReadyToStart()) { }
            await Task.FromResult(Task.CompletedTask);
        }

        private bool CooldownReady() => cooldownTask.Status == TaskStatus.RanToCompletion || cooldownTask.Status == TaskStatus.Created;
        private bool ReadyToStart() => currentAction.Status == TaskStatus.RanToCompletion || currentAction.Status == TaskStatus.Created;

        public class RateLimitException : Exception
        {
            public RateLimitException(string msg) : base(msg) { }
            public RateLimitException(string msg, Exception innerEx) : base(msg, innerEx) { }
        }
    }
}
