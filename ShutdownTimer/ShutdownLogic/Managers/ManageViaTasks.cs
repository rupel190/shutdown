using Shutdown.NotificationTray;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutdownLogic.Managers
{
    /**On Timers:
    * System.Timers.Timer, which fires an event and executes the code in one or
        more event sinks at regular intervals. The class is intended for use as a
        server-based or service component in a multithreaded environment; it has
        no user interface and is not visible at runtime.

    * System.Threading.Timer, which executes a single callback
        method on a thread pool thread at regular intervals.
        The callback method is defined when the timer is instantiated and cannot be
        changed. Like the System.Timers.Timer class, this class is intended for use
        as a server-based or service component in a multithreaded environment; 
        it has no user interface and is not visible at runtime.

        https://msdn.microsoft.com/en-us/library/system.threading.timer(v=vs.110).aspx
        https://stackoverflow.com/questions/1416803/system-timers-timer-vs-system-threading-timer
    */

    public sealed class ManageViaTasks : Manager
    {
        CancellationTokenSource cts;                //Cancel shutdown/sleep
        ConsoleEx console = new ConsoleEx();        //Custom console output

        #region Singleton
        //http://csharpindepth.com/Articles/General/Singleton.aspx
        //make this class sealed to prohibit inheritance
        //use .NET 4 Lazy<T> type and create the instance of this class
        //    -> why static: 
        //       outside a constructor, the "lazy" field doesn't exist yet, thus the lambda couldn't be executed in the first place
        //       would be non-static inside the constructor
        private static readonly Lazy<ManageViaTasks> lazy = new Lazy<ManageViaTasks>(() => new ManageViaTasks());
        //make static instance of/for this class so it can be called from outside
        public static ManageViaTasks Instance { get { return lazy.Value; } }
        //private constructor to avoid instantiating
        private ManageViaTasks() {
            
        }
        #endregion


        #region Overrides
        /// <summary>
        /// Aborts the Shutdown() or Sleep() method.
        /// </summary>
        public override void Abort(bool withInfo = false)
        {
            console.Write("Abort called!");
            if(cts != null)
                cts.Cancel();

            if (withInfo) {
                Display.Instance.Show("Warning", "Shutdown/Sleep aborted", ToolTipIcon.Warning);
            }
        }

        /// <summary>
        /// Provides the function to Shutdown the system after the specified amount of time. Can be Abort()ed.
        /// </summary>
        /// <param>Time is aggregated</param>
        public override void Shutdown(int hours, int minutes, int seconds)
        {
            console.Write("log");
            StatusShow(hours, minutes);
            ShutdownSleep(hours, minutes, seconds);
        }

        /// <summary>
        /// Provides the function to set the system to sleep state after the specified amount of time. Can be Abort()ed.
        /// </summary>
        /// <param>Time is aggregated</param>
        public override void Sleep(int hours, int minutes, int seconds)
        {
            StatusShow(hours, minutes, false);
            ShutdownSleep(hours, minutes, seconds, false);
        }
        #endregion

        private void StatusShow(int hours, int minutes, bool shutdown = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("in ");        
            //View as minutes if less than 2 hours
            if ((hours*60)+minutes > 120) {
                sb.Append(hours);
                sb.Append(" hours");
                sb.Append(", ");
                sb.Append(minutes);
            }
            else {
                sb.Append(hours * 60 + minutes);
            }
            sb.Append(" minutes");
            sb.Append("!");


            if (shutdown)
                Display.Instance.Show("Shutdown", sb.ToString());
            else
                Display.Instance.Show("Sleep", sb.ToString());
        }


        #region Wait and execute
        private void ShutdownSleep(int hours, int minutes, int seconds, bool shutdown = true)
        {
            console.Write("Shutdown called");
            int dueTime = (hours * 3600 + minutes * 60 + seconds) * 1000;

            cts = new CancellationTokenSource();
            CancellationToken token = new CancellationToken();
            token = cts.Token;
            token.Register(() => Abort());

            AsyncWait(token, dueTime);      
        }

        private async void AsyncWait(CancellationToken token, int dueTime, bool shutdown = true)
        {
            //Running this task asynchronously
            await Task.Run(async () =>
            {
                try {
                    //Running the delay asynchronously
                    console.Write("Async run");
                    //Token allows to break out of this inner task/lambda - it seems the token breaks the complete async() operation instead of just the "await" line!
                    //Furthermore using the token to cancel Task.Run doesn't do anything, could be because the delegate runs threaded
                    await Task.Delay(dueTime, token);
                    
                    //Execute the shutdown/sleep process after the time has run out
                    if(shutdown) {
                        Process.Start("ShutDown", "/s");
                    } else {
                        //why and how: https://superuser.com/questions/83437/hibernate-computer-with-a-timeout-from-command-line-on-windows-7
                        //Processinfo for the to-be-launched NirCMD
                        ProcessStartInfo nircmdinfo = new ProcessStartInfo();
                        //Path of NirCMD
                        nircmdinfo.FileName = @"../../../NirCMD\nircmd.exe";
                        nircmdinfo.Arguments = $"standby";
                        //Process.Start(nircmdinfo);
                    }
                } catch (TaskCanceledException ex) {
                    console.Write($"Shutdown/Sleep has been aborted: {ex.Message}");
                }
            });
        }
        #endregion
    }
}
