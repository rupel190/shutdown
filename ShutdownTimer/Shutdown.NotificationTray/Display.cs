using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutdown.NotificationTray
{
    public class Display
    {
        private NotifyIcon notifycon; //Used for displaying status messages

        private NotifyIcon Notifycon {
            get { return notifycon; }
            set { notifycon = value; }
        }

        private static readonly Lazy<Display> lazy = new Lazy<Display>(() => new Display());
        public static Display Instance { get { return lazy.Value; } }

        private Display()
        {
            notifycon = new NotifyIcon();
            notifycon.Icon = Properties.Resources.sleepdown;      //doesn't work if this icon isn't set as well!
        }

        public void Show(string balloonTipTitle, string balloonTipText, ToolTipIcon iconType=ToolTipIcon.Info)
        {
            Hide();
            //Text/Info for the icon
            notifycon.BalloonTipTitle = balloonTipTitle;
            notifycon.BalloonTipText = balloonTipText;

            notifycon.Visible = true;
            notifycon.ShowBalloonTip(2000);
        }

        public void Hide()
        {
            notifycon.Visible = false;
        }

        public void MouseClickSubscriber(MouseEventHandler sub)
        {
            notifycon.MouseClick += sub;
        }
        
    }
}
