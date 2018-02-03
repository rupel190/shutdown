using ShutdownLogic.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownLogic
{
    public class ManageViaDiagnostics : Manager
    {
        /// <summary>
        /// Uses the built-in commandline to set a planned shutdown.
        /// </summary>
        /// <param name="hours">Hours until shutdown is executed</param>
        /// <param name="minutes">Minutes until shutdown is executed</param>
        /// <param name="seconds">Seconds until shutdown is executed</param>
        public override void Shutdown(int hours, int minutes, int seconds)
        {
            seconds += minutes * 60;
            seconds += hours * 3600;
            Process.Start("ShutDown", $"/s /t {seconds}");
        }


        /// <summary>
        /// Launches the NirCMD commandline in order to execute a sleep command.
        /// This is necessary because planning sleep or hibernate is not possible using the built-in commandline. (Altough hibernate can be done using a workaround.)
        /// Furthermore the sleep command can ONLY be used per commandline if hibernate is disabled.
        /// In order to retain system settings NirCMD is used.
        /// </summary>
        /// <param name="hours">Hours until sleep is executed</param>
        /// <param name="minutes">Minutes until sleep is executed</param>
        /// <param name="seconds">Seconds until sleep is executed</param>
        public override void Sleep(int hours, int minutes, int seconds)
        {
            //why and how: https://superuser.com/questions/83437/hibernate-computer-with-a-timeout-from-command-line-on-windows-7
            seconds = (hours*3600 + minutes * 60 + seconds)*1000;

            //Find the correct path
            /*var content = Directory.GetFiles(@"../../../NirCMD");
            foreach (var c in content)
                Console.WriteLine(c);
            */

            //Processinfo for the to-be-launched NirCMD
            ProcessStartInfo nircmdinfo = new ProcessStartInfo();
            //Path of NirCMD
            nircmdinfo.FileName = @"../../../NirCMD\nircmd.exe";
            //Set arguments for NirCMD: Wait the specified number of MILLISECONDS, and then execute the specified NirCmd command.
            //nircmdinfo.Arguments = "cmdwait 5000 beep 200 500"; //cmdwait [time] beep-command [frequency] [duration]
            nircmdinfo.Arguments = $"cmdwait {seconds} standby";
            Process.Start(nircmdinfo);
        }
    }
}
