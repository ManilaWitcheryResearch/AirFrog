namespace Manila.AirFrog.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Manila.AirFrog.Common.Models;

    class MemoryStore
    {
        public Dictionary<string, McsMetaModel> McsGroup = new Dictionary<string, McsMetaModel>();
        public Dictionary<string, McsMonitoringModel> McsMonitoringGroup = new Dictionary<string, McsMonitoringModel>();
        public Dictionary<string, string> McsNameToId = new Dictionary<string, string>();
        public Dictionary<string, string> McsAddressToId = new Dictionary<string, string>();
    }
}
