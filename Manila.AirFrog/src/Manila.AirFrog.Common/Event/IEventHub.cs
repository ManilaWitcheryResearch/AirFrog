namespace Manila.AirFrog.Common.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventHub
    {
        bool Register(string name, Action<object> action, bool keep = true);
        void Emit(string eventname, object obj = null);
    }
}
