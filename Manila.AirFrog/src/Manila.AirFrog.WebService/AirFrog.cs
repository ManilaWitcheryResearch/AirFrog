namespace Manila.AirFrog.WebService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy.Hosting.Self;
    using Manila.AirFrog.Common;
    using Manila.AirFrog.Common.Core;
    using Manila.AirFrog.Common.Event;
    using Manila.AirFrog.TelegramBot;

    class AirFrog
    {
        private static NancyHost host;
        public static bool Inited { get; private set; }
        public static Logger LoggerMan;
        public static DataAccess DataAccess;
        public static IEventHub EventHub;
        public static TelegramBot TgBot;

        public static void RegisterEvents()
        {
            ChatEvents.Register(EventHub, DataAccess);
        }

        public static void Init()
        {
            var uri = new Uri("http://localhost:8000");
            LoggerMan = new Logger(String.Format("{0}.log", String.Format("{0:yyyy'-'MM'-'dd'_'HH'-'mm'-'ss}", DateTime.UtcNow)));
            host = new NancyHost(uri);
            DataAccess = new DataAccess();
            EventHub = LightEventHub.Instance;
            TgBot = new TelegramBot("202050640:AAFvS1MioQBZiIsAuWNbPTFtyNiGbfpJUAM", LoggerMan, EventHub);
            Inited = true;
            LoggerMan.Log("Successfully inited.");
        }

        public static bool Startup()
        {
            if (Inited == true)
            {
                LoggerMan.Log("Starting AirFrog WebService.");

                LoggerMan.Log("Starting Nancy.");
                host.Start();
                LoggerMan.Log("Nancy start successfully.");

                LoggerMan.Log("Starting Telegram Bot.");
                TgBot.Start();
                LoggerMan.Log("Telegram Bot start successfully.");

                LoggerMan.Log("Registering Buildin Events.");
                RegisterEvents();
                LoggerMan.Log("Buildin Events register successfully.");

                LoggerMan.Log("AirFrog WebService start successfully.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Stop()
        {
            LoggerMan.Log("Stopping AirFrog WebService.");

            host.Stop();  // stop hosting nancy
            TgBot.Stop(); // stop hosting tgbot

            LoggerMan.Log("AirFrog WebService stopped.");
        }
    }
}
