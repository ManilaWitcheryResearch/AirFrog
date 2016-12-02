namespace Manila.AirFrog.Common.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StdTerminal : BaseTerminal
    {
        protected override void OnCleanUp()
        {
            throw new NotImplementedException();
        }

        protected override bool OnInit()
        {
            throw new NotImplementedException();
        }

        protected override void OnInput(string cmd)
        {
            CmdExecutor.Instance.RunSync(cmd.Split(' ').ToList(), this);
        }

        protected override void OnOutput(string message)
        {
            throw new NotImplementedException();
        }

        public static StdTerminal CreateNewTerminal(string name)
        {
            StdTerminal terminal = new StdTerminal();
            if (BaseTerminal.CreateNewTerminal(name, terminal) == null)
            {
                return null;
            }
            return terminal;
        }

        private bool Execute(string cmd)
        {
            var parsed = cmd.Split(' ').ToList();
            // parse one
            ;
            return true;
        }
    }
}
