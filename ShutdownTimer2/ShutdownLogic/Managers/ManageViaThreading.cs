using System;
using System.Threading;
using System.Timers;

namespace ShutdownLogic.Managers
{
    public class ManageViaThreading : Manager
    {
        public override void Shutdown(int hours, int minutes, int seconds)
        {
            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.
            AutoResetEvent autoEvent = new AutoResetEvent(false);

            //var statusChecker = new StatusChecker(10);

            // Create a timer that invokes CheckStatus after one second, 
            // and every 1/4 second thereafter.
            Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n",
                              DateTime.Now);
            //var stateTimer = new System.Threading.Timer(statusChecker.CheckStatus,
            //                           autoEvent, 1000, 250);

            // When autoEvent signals, change the period to every half second.
            autoEvent.WaitOne();
            //stateTimer.Change(0, 500);
            Console.WriteLine("\nChanging period to .5 seconds.\n");

            // When autoEvent signals the second time, dispose of the timer.
            autoEvent.WaitOne();
            //stateTimer.Dispose();
            Console.WriteLine("\nDestroying timer.");
        }

        public override void Sleep(int hours, int minutes, int seconds)
        {
            throw new NotImplementedException();
        }
    }
}
