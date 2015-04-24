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

namespace IHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //--- CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();
        }

        //--- CONTROL MODE METHODS
        private void RadioBoxAuto_Click(object sender, RoutedEventArgs e)
        {
            // Disable controls
            ButtonStart.IsEnabled = true;
            ButtonZ.IsEnabled = false;
            ButtonS.IsEnabled = false;
            ButtonQ.IsEnabled = false;
            ButtonD.IsEnabled = false;

            // Tell the mindstorm auto mode is on by starting the thread
        }
        private void RadioBoxManual_Click(object sender, RoutedEventArgs e)
        {
            // Disable controls
            ButtonStart.IsEnabled = false;
            ButtonZ.IsEnabled = true;
            ButtonS.IsEnabled = true;
            ButtonQ.IsEnabled = true;
            ButtonD.IsEnabled = true;

            // Tell the mindstorm manual mode is on by stopping the thread for the auto mode.     
        }

        private void ButtonZ_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void ButtonS_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonQ_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Z:
                    ButtonZ.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.S:
                    ButtonS.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.Q:
                    ButtonQ.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                case Key.D:
                    ButtonD.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
                default:
                    break;
            }
        }
        //--- BUTTONS METHODS
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            // start thread here
        }

        
    }
}
