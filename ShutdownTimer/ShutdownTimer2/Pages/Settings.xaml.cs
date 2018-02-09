using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace ShutdownTimer.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        Configuration config;

        public Settings()
        {
            InitializeComponent();
            try {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cb_CloseWarning.IsChecked =  Boolean.Parse(config.AppSettings.Settings["CloseWarning"].Value);
                cb_MinimizeWindow.IsChecked =  Boolean.Parse(config.AppSettings.Settings["AutoMinimize"].Value);

            } catch (Exception ex) {
                MessageBox.Show($"Couldn't load config file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Allows convenient access to properties in the App.config file
        /// </summary>
        /// <param name="key">The key for which to get the value.</param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Allows convenient setting of properties in the App.config file.
        /// </summary>
        /// <param name="key">The key for which to set the value</param>
        /// <param name="value">The value associated with the key</param>
        public static void SetSetting(string key, string value)
        {
            //the config file to use, besides the exe
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //change the settings
            configuration.AppSettings.Settings[key].Value = value;
            //save+savemode
            configuration.Save(ConfigurationSaveMode.Full, true);
            //refresh so new values can be read
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void cb_CloseWarning_Checked(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Changed app settings to " + cb_CloseWarning.IsChecked.ToString());
            SetSetting("CloseWarning", "true");
        }

        private void cb_CloseWarning_Unchecked(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Changed app settings to " + cb_CloseWarning.IsChecked.ToString());
            SetSetting("CloseWarning", "false");
        }

        private void cb_minimizeWindow_Checked(object sender, RoutedEventArgs e)
        {
            SetSetting("AutoMinimize", "true");
        }

        private void cb_minimizeWindow_Unchecked(object sender, RoutedEventArgs e)
        {
            SetSetting("AutoMinimize", "false");
        }
    }
}
