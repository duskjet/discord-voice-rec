using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.API.Socket
{
    public class RateLimit
    {
        private const int interval = 500;

        private bool ready = false;
        private Task cooldownTask;

        public bool SendingAllowed { get; private set; }

        public void Fire()
        {
            ready = false;

            Cooldown();
        }

        private void Cooldown() // классный даун
        {
            if (cooldownTask.Status == TaskStatus.RanToCompletion)
                cooldownTask = Task.Run(async () =>
                {
                    await Task.Delay(interval);
                    ready = true;
                });
        }
    }
}
