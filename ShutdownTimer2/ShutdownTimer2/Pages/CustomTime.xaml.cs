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
    /// Interaction logic for CustomTime.xaml
    /// </summary>
    public partial class CustomTime : Page
    {
        int hours = 0, minutes = 0, seconds = 0;
        readonly string errmsg = "Couldn't retrieve a value!";

        public CustomTime()
        {
            InitializeComponent();
        }

        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Parse values
            ManageViaDiagnostics manager = new ManageViaDiagnostics();
            if(!string.IsNullOrEmpty(tfHours.Text))
                if (!Int32.TryParse(tfHours.Text, out hours))
                    MessageBox.Show(errmsg, "Minutes", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (!string.IsNullOrEmpty(tfMinutes.Text))
                if (!Int32.TryParse(tfMinutes.Text, out minutes))
                    MessageBox.Show(errmsg, "Minutes", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (!string.IsNullOrEmpty(tfSeconds.Text))
                if (!Int32.TryParse(tfSeconds.Text, out seconds))
                    MessageBox.Show(errmsg,"Seconds", MessageBoxButton.OK, MessageBoxImage.Warning);


            MessageBoxResult ds = MessageBoxResult.Yes;
            //Check if empty and change messageboxresult if required
            if(hours == 0 && minutes == 0 && seconds == 0) {
                ds = MessageBox.Show("Instant shutdown ahead! Proceed?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            //Not zero or confirmed
            if(ds == MessageBoxResult.Yes){
                //Save as preset
                if(cb_SaveAsPreset.IsChecked.Value) {
                    throw new NotImplementedException();
                }

                //Issue shutdown
                if (cb_Hibernate.IsChecked.Value) {
                    manager.Abort();
                    manager.Hibernate(hours, minutes, seconds);
                }
                else {
                    manager.Shutdown(hours, minutes, seconds);
                    manager.Abort();
                }
            } 
        }
    }
}
