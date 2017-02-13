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
        public Dictionary<string, BasicUserModel> BasicUserGroup = new Dictionary<string, BasicUserModel>();
    }
}
