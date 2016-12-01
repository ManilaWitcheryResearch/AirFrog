namespace Manila.AirFrog.Common.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class TerminalManager
    {
        public Dictionary<string, BaseTerminal> terminalList;

        private static TerminalManager instance = null;

        public static TerminalManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TerminalManager();
                }
                return instance;
            }
        }

        public bool RegisterNewTerminal(string name, BaseTerminal terminal)
        {
            return true;
        }

    }
}
