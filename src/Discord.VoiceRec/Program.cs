using NLog;
using System;

namespace Discord.VoiceRec
{
    public class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            log.Debug("Sample debug log");
            Console.ReadLine();
        }
    }
}
