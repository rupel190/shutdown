using Shutdown.NotificationTray;
using ShutdownLogic;
using ShutdownLogic.Managers;
using ShutdownTimer.Pages;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace ShutdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Display notificationDisplay = Display.Instance;

        public MainWindow()
        {
            InitializeComponent();
            MinHeight = 500;
            MinWidth = 525;
            NavigationFrame.Navigate(new Presets());

            notificationDisplay.Notifycon.MouseClick += notifyIcon_Click;
            StateChanged += WindowStateChanged;
            Closing += WindowClosing;
            Closed += notifyIcon_OnClosing;
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            var appSettings = ConfigurationManager.AppSettings;

            if(appSettings["CloseWarning"] == "true") {
                e.Cancel = true;
                var answer = System.Windows.MessageBox.Show("Running timers will terminate. Turn this warning off in the settings page.\n\nContinue?", "Exit", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (answer == MessageBoxResult.Yes)
                    e.Cancel = false;
            }
        }

        #region NotifyIcon
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            //Show program again on click
            Show();
            WindowState = WindowState.Normal;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            
            //Display notify icon in status bar
            if (WindowState == WindowState.Minimized) {
                //notifycon.Show("Sleepdown minimized to tray", "Don't close the program if a countdown is active!");
                notificationDisplay.Notifycon.BalloonTipTitle = "hahah";
                notificationDisplay.Notifycon.BalloonTipText = "hfahsh";
                notificationDisplay.Notifycon.Visible = true;
                Hide();
            }
            else {
                notificationDisplay.Notifycon.Visible = false;
                Show();
            }
        }

        private void notifyIcon_OnClosing(object sender, EventArgs e)
        {
            /*
            Remove icon when exiting program - this can only happen when the process is terminated
            because the icon is only shown when minimized where there is no option to exit.
            This may change, also exiting through the taskmanager may happen without any problems now.
            */
            Display.Instance.Notifycon.Visible = false;
        }
        #endregion


        #region ScaleValue Dependency Property - used for zoom on resize
        public static readonly DependencyProperty ScaleValueProperty =
            DependencyProperty.Register(
                "ScaleValue",
                typeof(double),
                typeof(MainWindow),
                new UIPropertyMetadata(
                    1.0,
                    new PropertyChangedCallback(OnScaleValueChanged),
                    new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            Console.WriteLine("Coercing scale value");
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                return mainWindow.OnCoerceScaleValue((double)value);
            else
                return value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("Window scale has changed");
            MainWindow mainWindow = o as MainWindow;
            if (mainWindow != null)
                mainWindow.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            Console.WriteLine("Coercing scale value");
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {

        }

        public double ScaleValue {
            get {
                Console.WriteLine("Scale value pulled");
                return (double)GetValue(ScaleValueProperty);
            }
            set {
                Console.WriteLine("Scale value set");
                SetValue(ScaleValueProperty, value);
            }
        }
        #endregion

        private void MainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("Mainwindow size changed");
            CalculateScale();
        }

        private void CalculateScale()
        {
            Console.WriteLine("Calculating new scale");
            double yScale = ActualHeight / 500f;
            double xScale = ActualWidth / 200f;
            double value = Math.Min(xScale, yScale);
            ScaleValue = (double)OnCoerceScaleValue(myMainWindow, value);
        }


        #region Buttons

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
            Manager manager = ManageViaTasks.Instance;
            manager.Abort(true);
        }

        /// <summary>
        /// Increases size of all elements
        /// </summary>
        private void btnIncZoom_Click(object sender, RoutedEventArgs e)
        {
            myMainWindow.Height += 100;
            myMainWindow.Width += 150;
        }

        /// <summary>
        /// Decreases size of all elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecZoom_Click(object sender, RoutedEventArgs e)
        {
            if(myMainWindow.Height-100 > myMainWindow.MinHeight)
                myMainWindow.Height -= 100;
            if(myMainWindow.Width- 150 > myMainWindow.MinWidth)
                myMainWindow.Width -= 150;
        }

        #endregion

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationFrame.Navigate(new Settings());
        }
    }
}
