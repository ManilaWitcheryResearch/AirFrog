namespace Manila.AirFrog.Common.Terminal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BaseTerminal : IBaseTerminal, IDisposable
    {
        public string Name { get; private set; }
        public DateTime LastActiveTime { get; private set; }
        public DateTime LastInputTime { get; private set; }

        protected abstract bool OnInit();

        protected abstract void OnCleanUp();

        protected abstract void OnOutput(string message);

        protected abstract void OnInput(string cmd);

        public static BaseTerminal CreateNewTerminal(string name, BaseTerminal terminal)
        {
            terminal.Name = name;
            if (TerminalManager.Instance.RegisterNewTerminal(name, terminal))
            {
                return terminal;
            }
            else
            {
                return null;
            }
        }
        
        #region Common Actions

        protected BaseTerminal()
        {
            this.LastActiveTime = DateTime.UtcNow;
            this.LastInputTime = DateTime.UtcNow;
            this.OnInit();
        }

        public void InputLine(string line)
        {
            this.OnInput(line);
        }

        public void OutputLine(string line)
        {
            this.OnOutput(line);
        }

        public void Close()
        {
            this.Dispose();
        }

        #endregion Common Actions

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                this.OnCleanUp();

                disposedValue = true;
            }
        }

        ~BaseTerminal()
        {
            Dispose(false);
        }

        public void Dispose()
        {
                        Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
