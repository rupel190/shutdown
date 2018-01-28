using ShutdownLogic;
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

namespace ShutdownTimer2
{
    /// <summary>
    /// Interaction logic for Presets.xaml
    /// </summary>
    public partial class Presets : Page
    {
        ManageViaDiagnostics manager;

        public Presets()
        {
            InitializeComponent();
            manager = new ManageViaDiagnostics();
        }

        private void btShutdown60min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Shutdown(0, 60, 0);
        }

        private void btShutdown90min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Shutdown(0, 90, 0);
        }

        private void btShutdown120min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Shutdown(0, 120,0);
        }

        private void btHibernate60min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Hibernate(0, 60, 0);
        }

        private void btHibernate90min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Hibernate(0, 90, 0);
        }

        private void btHibernate120min_Click(object sender, RoutedEventArgs e)
        {
            manager.Abort();
            manager.Hibernate(0, 120, 0);
        }
    }
}
