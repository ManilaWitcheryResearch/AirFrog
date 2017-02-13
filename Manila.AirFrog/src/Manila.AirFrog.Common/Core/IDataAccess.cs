namespace Manila.AirFrog.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Threading.Tasks;
    using Manila.AirFrog.Common.Models;
    using Newtonsoft.Json;

    public interface IDataAccess
    {
        McsMetaModel QueryServerInfo(string serverId);

        McsMonitoringModel QueryServerMonitoringInfo(string serverId);

        McsMetaModel RegisterNewServer(McsMetaModel mcsInfo, bool inner = false);

        void UpdateServerInfo(McsMetaModel mcsInfo, McsMetaModelMask mask);

        void UpdateServerMonitoringInfo(McsMonitoringModel mcsInfo, McsMonitoringModelMask mask);

        void UpdateServerLastSeen(string serverId);

        string ExecuteCommandOnServer(string serverId, string cmd);

        void SendChatMsgToMcs(StdChatModel obj, string ignoreId = "!!DONT CHANGE THIS!!");

        void SendHeartbeatToMcs();
    }

}