namespace Manila.AirFrog.WebService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy.Hosting.Self;
    using Manila.AirFrog.Common;

    class AirFrog
    {
        public static bool Inited = false;
        public static Logger LoggerMan;
        private static NancyHost host;

        public static void Init()
        {
            var uri = new Uri("http://localhost:8000");
            LoggerMan = new Logger(String.Format("{0}.log", String.Format("{0:yyyy'-'MM'-'dd'_'HH'-'mm'-'ss}", DateTime.UtcNow)));
            host = new NancyHost(uri);
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

            LoggerMan.Log("AirFrog WebService stopped.");
        }
    }
}
