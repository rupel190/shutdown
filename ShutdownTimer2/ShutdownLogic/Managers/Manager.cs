using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownLogic.Managers
{
    public abstract class Manager
    {
        /// <summary>
        /// Check if the shutdown process is currently running
        /// </summary>
        /// <returns>Bool value indicating if the process is running</returns>
        public virtual bool isActive()
        {
            var running = Process.GetProcessesByName("ShutDown");
            if (running.Length > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Aborts the currently running shutdown process IF it's running.
        /// Does nothing otherwise.
        /// </summary>
        public virtual void Abort()
        {
            //Should be possible to just abort the process itself, but it's easier to use the built-in function
            Process.Start("Shutdown", "/a");
        }

        public abstract void Shutdown(int hours, int minutes, int seconds);

        public abstract void Sleep(int hours, int minutes, int seconds);

    }
}
