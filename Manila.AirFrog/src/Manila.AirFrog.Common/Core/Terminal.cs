namespace Manila.AirFrog.Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Manila.AirFrog.Common.Models;

    class TerminalManager
    {
        public Dictionary<string, BaseTerminal> terminalList;


    }

    class BaseTerminal
    {
        public DateTime LastActiveTime { get; private set; }
        public DateTime LastInputTime { get; private set; }

        public BaseTerminal()
        {
            ;
        }

        public void InputLine(string line)
        {
            ;
        }

        // hook on ( in/out )
    }

    class StdTerminal : BaseTerminal
    {
        public StdTerminal()
        {
            ;
        }

        public string RunCommand(string cmd)
        {
            ;
            return "";
        }

        public void Close()
        {
            ;
        }
    }
}
