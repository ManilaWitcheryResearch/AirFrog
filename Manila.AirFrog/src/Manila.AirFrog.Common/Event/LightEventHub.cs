namespace Manila.AirFrog.Common.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LightEventHub : IEventHub
    {
        private Dictionary<string, Action<string>> _dict;
        private Dictionary<string, bool> _keep;

        public LightEventHub()
        {
            _dict = new Dictionary<string, Action<string>>();
            _keep = new Dictionary<string, bool>();
        }

        public bool Emit(string name, Action<string> action, bool keep = true)
        {
            if (_dict.ContainsKey(name))
            {
                // better using exception.
                // throw new Exception("Duplicated event name.");
                return false;
            }
            else
            {
                _dict.Add(name, action);
                _keep.Add(name, keep);
                return true;
            }
        }

        public void Trigger(string eventname)
        {
            if (_dict.ContainsKey(eventname))
            {
                _dict[eventname]("");
                if (_keep[eventname] == false)
                {
                    _dict.Remove(eventname);
                    _keep.Remove(eventname);
                }
            }
            else
            {
                // throw exception
                throw new Exception("Invalid event name.");
            }
        }

    }
}
