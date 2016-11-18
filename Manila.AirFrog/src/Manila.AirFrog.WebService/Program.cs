namespace Manila.AirFrog.WebService
{
    using System;
    using Mono.Unix;
    using Mono.Unix.Native;
    using Manila.AirFrog.WebService.Libs;

    class Program
    {
        static void Main(string[] args)
        {
            AirFrog.Init();
            if (AirFrog.Startup())
            {
                Console.WriteLine("Press any [Enter] to close the host.");
                // check if we're running on mono
                if (Utility.CheckIfRunningOnMono())
                {
                    // on mono, processes will usually run as daemons - this allows you to listen
                    // for termination signals (ctrl+c, shutdown, etc) and finalize correctly
                    UnixSignal.WaitAny(new[] {
                        new UnixSignal(Signum.SIGINT),
                        new UnixSignal(Signum.SIGTERM),
                        new UnixSignal(Signum.SIGQUIT),
                        new UnixSignal(Signum.SIGHUP)
                    });
                }
                else
                {
                    Console.ReadLine();
                }
            }

            AirFrog.LoggerMan.LogErr("Stopping AirFrog by hand or system signal.");
            AirFrog.Stop();
        }
    }
}
