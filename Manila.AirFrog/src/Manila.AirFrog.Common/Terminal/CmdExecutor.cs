namespace Manila.AirFrog.Common.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class CmdExecutor
    {
        private Dictionary<string, Action<List<string>, IBaseTerminal>> cmdParser;
        private CmdExecutor instance = null;

        #region Buildin Actions
        //private Action<List<string>, IBaseTerminal> cmdHelpText = ((x, y) => );
        private void CmdHelpText(List<string> cmd, IBaseTerminal term)
        {
            term.OutputLine("?\tType to Monitor this message");
            // TODO: add another class for format actions.
        }
        #endregion Buildin Actions

        private CmdExecutor()
        {
            //cmdParser.Add("?", () => )
        }

        public CmdExecutor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CmdExecutor();
                }
                return instance;
            }
        }
    }
}
