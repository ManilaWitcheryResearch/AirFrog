namespace Manila.AirFrog.Common.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBaseTerminal
    {
        void InputLine(string line);

        void OutputLine(string line);
    }
}
