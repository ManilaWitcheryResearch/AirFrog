namespace Manila.AirFrog.Common.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Command
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string CommandText { get; private set; }

        private Action<List<string>, IBaseTerminal> action;

        public Command(string name, string commandText, string description, Action<List<string>, IBaseTerminal> action)
        {
            Name = name;
            Description = description;
            CommandText = commandText;
            this.action = action;
        }

        public void RunSync(List<string> cmd, IBaseTerminal term)
        {
            action(cmd, term);
        }

        public async Task RunAsync(List<string> cmd, IBaseTerminal term)
        {
            await Task.Run(() => action(cmd, term)).ConfigureAwait(false);
        }
    }

    class CmdExecutor
    {
        private Dictionary<string, Command> cmdList = new Dictionary<string, Command>();
        private static CmdExecutor instance = null;

        #region Buildin Actions
        // note: basic actions must be sync
        private void CmdHelpText(List<string> cmd, IBaseTerminal term)
        {
            term.OutputLine("Commands\tDescription");
            // TODO: add another class for format actions.
            // TODO: add mutex for terminal output
            // TODO: next feature add parallel tasks & batch process & task manager
            foreach (var singleCmd in cmdList)
            {
                term.OutputLine(string.Format("{0}\t{1}", singleCmd.Value.CommandText, singleCmd.Value.Description));
            }
        }

        private void McsList(List<string> cmd, IBaseTerminal term)
        {
            ;
            // TODO: implement this function
        }

        private void RegisterBuildinCmds()
        {
            cmdList.Add("?", new Command("?", "?", "Type to Monitor this message", CmdHelpText));
            cmdList.Add("help", new Command("help", "help", "Type to Monitor this message", CmdHelpText));

        }
        #endregion Buildin Actions

        private CmdExecutor()
        {
            RegisterBuildinCmds();
        }

        public void RunSync(List<string> cmd, IBaseTerminal term)
        {
            try
            {
                cmdList[cmd[0]].RunSync(cmd.Skip(1).ToList(), term);
            }
            catch (Exception)
            {
                term.OutputLine("Error in execute the command");
            }
        }

        public async Task RunAsync(List<string> cmd, IBaseTerminal term)
        {
            try
            {
                await cmdList[cmd[0]].RunAsync(cmd.Skip(1).ToList(), term).ConfigureAwait(false);
            }
            catch (Exception)
            {
                term.OutputLine("Error in execute the command");
            }
        }

        public static CmdExecutor Instance
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
