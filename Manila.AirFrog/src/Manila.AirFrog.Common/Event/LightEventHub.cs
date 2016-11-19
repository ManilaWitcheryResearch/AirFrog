namespace Manila.AirFrog.Common.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LightEventHub : IEventHub
    {
        private Dictionary<string, Action<object>> _dict;
        private Dictionary<string, bool> _keep;
        private ILogger Logger;
        static private LightEventHub instance = null;

        static public LightEventHub Instance {
            get
            {
                if (instance == null)
                {
                    instance = new LightEventHub();
                }
                return instance;
            }
        }

        public LightEventHub(ILogger logger = null)
        {
            _dict = new Dictionary<string, Action<object>>();
            _keep = new Dictionary<string, bool>();
            Logger = logger == null ? new Logger("") : logger;
        }

        public bool Register(string name, Action<object> action, bool keep = true)
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

        public void Emit(string eventname, object obj = null)
        {
            if (_dict.ContainsKey(eventname))
            {
                Logger.Log(string.Format("Event {0} emitted.", eventname));
                Task.Run(() => _dict[eventname](obj));
                if (_keep[eventname] == false)
                {
                    _dict.Remove(eventname);
                    _keep.Remove(eventname);
                }

            }
            else
            {
                Logger.LogErr(string.Format("Event {0} emitted but not registered.", eventname));
                // throw exception
                // throw new Exception("Invalid event name.");
            }
        }

    }
}
