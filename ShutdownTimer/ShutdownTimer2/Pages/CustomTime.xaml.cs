using ShutdownLogic;
using ShutdownLogic.Managers;
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
    /// Interaction logic for CustomTime.xaml
    /// </summary>
    public partial class CustomTime : Page
    {
        int hours = 0, minutes = 0, seconds = 0;
        readonly string errmsg = "Couldn't retrieve a value!";
        Manager manager;

        public CustomTime()
        {
            InitializeComponent();
            manager = ManageViaTasks.Instance;

            //Pressing Enter confirms the inputs
            tfHours.PreviewKeyDown += TfHours_PreviewKeyDown;
            tfMinutes.PreviewKeyDown += TfMinutes_PreviewKeyDown;
            tfSeconds.PreviewKeyDown += TfSeconds_PreviewKeyDown;
        }

        #region Enter to confirm
        private void TfHours_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) {
                btConfirm_Click(sender, e);
            }
        }

        private void TfMinutes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                btConfirm_Click(sender, e);
            }
        }

        private void TfSeconds_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                btConfirm_Click(sender, e);
            }
        }

        #endregion

        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Parse values
            if(!string.IsNullOrEmpty(tfHours.Text))
                if (!Int32.TryParse(tfHours.Text, out hours))
                    MessageBox.Show(errmsg, "Hours", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (!string.IsNullOrEmpty(tfMinutes.Text))
                if (!Int32.TryParse(tfMinutes.Text, out minutes))
                    MessageBox.Show(errmsg, "Minutes", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (!string.IsNullOrEmpty(tfSeconds.Text))
                if (!Int32.TryParse(tfSeconds.Text, out seconds))
                    MessageBox.Show(errmsg,"Seconds", MessageBoxButton.OK, MessageBoxImage.Warning);


            MessageBoxResult ds = MessageBoxResult.Yes;
            //Check if empty and change messageboxresult if required
            if(hours == 0 && minutes == 0 && seconds == 0) {
                ds = MessageBox.Show("Instant shutdown ahead! Proceed?", "Sleepdown: Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            //Not zero or confirmed
            if(ds == MessageBoxResult.Yes){
                //Save as preset
                if(cb_SaveAsPreset.IsChecked.Value) {
                    throw new NotImplementedException();
                }

                //Issue shutdown
                if (cb_Hibernate.IsChecked.Value)
                    manager.Sleep(hours, minutes, seconds);
                else
                    manager.Shutdown(hours, minutes, seconds);
            } 
        }
    }
}
