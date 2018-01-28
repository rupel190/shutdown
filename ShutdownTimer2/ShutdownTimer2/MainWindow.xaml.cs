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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationFrame.Navigate(new Presets());
        }

        /// <summary>
        /// navigates to presets menu
        /// </summary>
        private void Presets_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new Presets());
        }

        /// <summary>
        /// Navigates to Custom time input menu
        /// </summary>
        private void CustomTime_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new CustomTime());
        }

        /// <summary>
        /// Aborts the pending shutdown
        /// </summary>
        private void AbortAll_Button_Click(object sender, RoutedEventArgs e)
        {
            ManageViaDiagnostics diaman = new ManageViaDiagnostics();
            diaman.Abort();
        }
    }
}
