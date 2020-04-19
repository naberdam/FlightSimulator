using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// This class is the HomePage of this program.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // Boolean type that tells us if we want to close up or not.
        bool closeApp = false;
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        // Function that set the default value of Ip and Port from the AppSettings.
        private void Button_Set_Default(object sender, RoutedEventArgs e)
        {
            ip.Text = ConfigurationManager.AppSettings["IP"].ToString();
            port.Text = ConfigurationManager.AppSettings["Port"].ToString();
        }
        // Function that login to our game.
        private void Button_Login(object sender, RoutedEventArgs e)
        {
            try
            {
                // Connect to the model.
                (Application.Current as App).SimulatorModel.Connect(ip.Text, int.Parse(port.Text));
                // Create the SubMainMenu.
                SubMainMenu view = new SubMainMenu();
                // Change the boolean type to true for closing the program.
                closeApp = true;
                this.Close();
                // Show the SubMainMenu.
                view.Show();
            }// Something got wrong
            catch (Exception)
            {
                string message = String.Format("The server or ip are not good, please try again.\n If you want, " +
                    "you can try the default ip and port");
                MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        // Exit function.
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            closeApp = true;
            this.Close();
            Application.Current.Shutdown();
        }
        // Function that works everytime that we want to close this window.
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!closeApp)
            {
                e.Cancel = true;
                base.OnClosing(e);
            }
        }
        // A function that eliminates the buttons.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        // Function that works when the MouseDown and then the user can move the window where ever he wants.
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
