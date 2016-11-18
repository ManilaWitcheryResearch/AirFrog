using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.AirFrog.WebService.Modules
{
    using Nancy;
    public class MonitorModule : NancyModule
    {
        public MonitorModule()
        {
            Get["/api/mcs/mcchatmsg"] = parameters =>
            {
                return ".";
            };
        }
    }
}
