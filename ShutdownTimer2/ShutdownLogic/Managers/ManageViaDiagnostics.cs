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
        public override void Shutdown(int hours, int minutes, int seconds)
        {
            seconds += minutes * 60;
            seconds += hours * 3600;
            Process.Start("ShutDown", $"/s /t {seconds}");
        }


        public override void Hibernate(int hours, int minutes, int seconds)
        {
            throw new NotImplementedException();
            //workaround: https://superuser.com/questions/83437/hibernate-computer-with-a-timeout-from-command-line-on-windows-7
            seconds += minutes * 60;
            seconds += hours * 3600;
            //Process.Start("CMD.exe", $"/K timeout /t {seconds} /NOBREAK > NUL && shutdown /h");

            var content = Directory.GetFiles(@"../../../NirCMD");
            foreach (var c in content)
                Console.WriteLine(c);

            ProcessStartInfo nircmdinfo = new ProcessStartInfo();
            nircmdinfo.FileName = @"../../../NirCMD\nircmd.exe";
            nircmdinfo.Arguments = $"cmdwait {seconds} standby";
            Process.Start(nircmdinfo);
            
        }
    }
}
