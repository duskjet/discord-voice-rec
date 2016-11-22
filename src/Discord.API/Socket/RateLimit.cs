using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using debug = System.Diagnostics.Debug;
namespace Discord.API.Socket
{
    public class RateLimit
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public RateLimit(int interval = 500, int opm = 120)
        {
            this.interval = interval;
            this.opm = this.opmCurrent = opm;
            this.time = minute;

            checkpoint = DateTime.Now.AddMilliseconds(minute);
            timer = new Timer(CalculateTimeCallback, new AutoResetEvent(false), 0, 250);
        }

        public async Task Wait(bool isPriority = false)
        {
            var sw = new Stopwatch(); sw.Start();
            var heartbeat = new Payload<Payloads.Gateway.Heartbeat>(null);
            var delay = GetDelay(isPriority);
            await Task.Delay(delay);

            if (opmCurrent == 0)
            {
                log.Debug("Operation count is zero");
                debug.WriteLine("OPM is zero");
                while (opmCurrent <= 0) ;
            }
            //if (opmCurrent <= 0)
            //{
            //    throw new InvalidOperationException("Current OPM value is zero or less");
            //}

            // Decrease operations by one
            opmCurrent--;

            log.Debug($"Wait complete in {sw.ElapsedMilliseconds}ms. Remaining operations: {opmCurrent}"); 
        }

        private int GetDelay(bool isPriority)
        {
            var delay = interval;

            if (opmCurrent == 0) return minute;
            else 
            if (!isPriority) delay += TimeForNextOperation();

            return delay;
        }

        private int TimeForNextOperation() => minute / opmCurrent;

        private void CalculateTimeCallback(object stateInfo)
        {
            if (checkpoint.CompareTo(DateTime.Now) < 0)
            {
                log.Debug("Renew operations");

                checkpoint = DateTime.Now.AddMilliseconds(minute);
                time = minute;

                opmCurrent = opm;
                return;
            }

            time -= 250;
        }

        int interval, opm, opmCurrent, time;
        const int minute = 5 * 1000;
        Timer timer;
        DateTime checkpoint;
    }
}
