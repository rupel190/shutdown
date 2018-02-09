using ShutdownLogic;
using ShutdownLogic.Managers;
using ShutdownTimer.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShutdownTimer
{
    /// <summary>
    /// Interaction logic for Presets.xaml
    /// </summary>
    public partial class Presets : Page
    {
        Manager manager;

        public Presets()
        {
            InitializeComponent();
            //manager = new ManageViaDiagnostics(); //replaced with (static) ManageViaThreading
            manager = ManageViaTasks.Instance;
        }

        private void CheckForClose()
        {
            if(Settings.GetSetting("AutoMinimize") == "true") {
                //HOLY CRISP IT WORKS!!!
                MainWindow.GetWindow(this).WindowState = WindowState.Minimized;
            }
        }

        private void btShutdown60min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Shutdown(0, 60, 0);
            CheckForClose();
        }

        private void btShutdown90min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Shutdown(0, 90, 0);
            CheckForClose();
        }

        private void btShutdown120min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Shutdown(0, 120,0);
            CheckForClose();
        }

        private void btHibernate60min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Sleep(0, 60, 0);
            CheckForClose();
        }

        private void btHibernate90min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Sleep(0, 90, 0);
            CheckForClose();
        }

        private void btHibernate120min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Sleep(0, 120, 0);
            CheckForClose();
        }
    }
}
