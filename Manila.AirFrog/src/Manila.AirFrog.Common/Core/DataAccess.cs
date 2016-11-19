namespace Manila.AirFrog.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Threading.Tasks;
    using Manila.AirFrog.Common.Models;

    public class DataAccess
    {
        private MemoryStore mStore;
        private ILogger Logger;
        public DataAccess()
        {
            ;
        }

        public McsMetaModel RegisterNewServer(McsMetaModel mcsInfo, bool inner = false)
        {
            if (inner)
            {
                if (string.IsNullOrEmpty(mcsInfo.ServerId))
                {
                    // throw new Exception("No serverid found at mcs inner register.");
                    return null;
                }
                mStore.McsGroup[mcsInfo.ServerId] = mcsInfo;
                if (mStore.McsMonitoringGroup.ContainsKey(mcsInfo.ServerId))
                {
                    return mcsInfo;
                }
                else
                {
                    mStore.McsMonitoringGroup[mcsInfo.ServerId] = new McsMonitoringModel
                    {
                        ServerId = mcsInfo.ServerId,
                        Status = "new",
                        LastSeen = DateTime.UtcNow,
                    };
                }
            }
            else
            {
                var x = mStore.McsGroup.Select(i => i.Value).Where(d => d.Name == mcsInfo.Name).ToList();
                if (x.Count == 0)
                {
                    Guid serverId = Guid.NewGuid();
                    mcsInfo.ServerId = serverId.ToString("N");
                    mStore.McsGroup[mcsInfo.ServerId] = mcsInfo;
                    mStore.McsMonitoringGroup[mcsInfo.ServerId] = new McsMonitoringModel
                    {
                        ServerId = mcsInfo.ServerId,
                        Status = "new",
                        LastSeen = DateTime.UtcNow,
                    };
                }
                else
                {
                    return x[0];
                }
            }
            return null;
        }

        public void UpdateServerInfo(McsMetaModel mcsInfo, McsMetaModelMask mask)
        {
            try
            {
                //// Way1
                //var SomeObject = new { .../*properties*/... };
                //var PropertyInfos = SomeObject.GetType().GetProperties();
                //foreach (PropertyInfo pInfo in PropertyInfos)
                //{
                //    string propertyName = pInfo.Name; //gets the name of the property
                //    doSomething(pInfo.GetValue(SomeObject, null));
                //}
                //// Way2
                //mcsInfo.GetType().GetProperties().ToList().ForEach(p => {
                //    //p is each PropertyInfo
                //    DoSomething(p);
                //});

                mStore.McsGroup[mcsInfo.ServerId].ApplyUpdate(mcsInfo, mask);
                UpdateServerLastSeen(mcsInfo.ServerId);

            }
            catch (Exception e)
            {
                Logger.LogErr(e.ToString());
            }
        }

        public void UpdateServerMonitoringInfo(McsMonitoringModel mcsInfo, McsMonitoringModelMask mask)
        {
            try
            {
                mStore.McsMonitoringGroup[mcsInfo.ServerId].ApplyUpdate(mcsInfo, mask);
                mStore.McsMonitoringGroup[mcsInfo.ServerId].LastSeen = DateTime.UtcNow;
            }
            catch (Exception e)
            {
                Logger.LogErr(e.ToString());
            }
        }

        public void UpdateServerLastSeen(string serverId)
        {
            try
            {
                mStore.McsMonitoringGroup[serverId].LastSeen = DateTime.UtcNow;
            }
            catch (Exception e)
            {
                Logger.LogErr(e.ToString());
            }
        }
    }
}
