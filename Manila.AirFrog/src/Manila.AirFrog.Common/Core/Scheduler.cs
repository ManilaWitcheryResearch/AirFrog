namespace Manila.AirFrog.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using Manila.AirFrog.Common.Models;

    class Scheduler
    {
        static private Scheduler instance = null;

        static public Scheduler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Scheduler();
                }
                return instance;
            }
        }

        private Scheduler()
        {
            ;
            // TODO: replace DataAccess by IDataAccess.
        }

        private Timer MonitoringTimer = null;

        public void ScheduleUpBuildinTasks()
        {
            MonitoringTimer = new Timer((object x) => {
                DataAccess.Instance.SendHeartbeatToMcs();
            }, null, new TimeSpan(0, 1, 0), new TimeSpan(0, 5, 0));
        }

        public void StopBuildinTasks()
        {
            MonitoringTimer.Dispose();
        }
    }
}
