namespace Manila.AirFrog.Common.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventHub
    {
        bool Emit(string name, Action<string> action, bool keep = true);
        void Trigger(string eventname);
    }
}
