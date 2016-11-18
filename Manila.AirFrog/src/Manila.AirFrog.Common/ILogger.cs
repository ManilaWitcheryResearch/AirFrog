using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.AirFrog.Common
{
    public interface ILogger
    {
        void Log(string msg);

        void LogErr(string msg);
    }
}
