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
using SensorsAlgorithm;
using System.Windows.Threading;

namespace IHM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //--- PROPERTIES
        private Boolean _autoModeIsOn;
        private Boolean _goalAchieved;

        private SensorControler _controler;
        private Dispatcher _dispatcher;
        
        //--- CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();

            _controler = new SensorControler("usb");

            _controler.ColorSensor.OnColorChanged += AutoMode;
            //_ultrasonicSensor.OnDistanceChanged += AutoMode;

            _dispatcher = Application.Current.Dispatcher;
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
            _autoModeIsOn = true;
            
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
            _autoModeIsOn = false;

            // Stop Car driving thread
            _controler.StopThread();
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

            // Start Car driving thread
            _controler.StopThread();
            this.Close();
        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            // start thread here

            // Start Car driving thread
            _controler.StartThread();
        }

        //--- AUTO MODE MANAGEMENT
        public Boolean AutoModeIsOn
        {
            get
            {
                return _autoModeIsOn;
            }
            set
            {
                _autoModeIsOn = value;
            }
        }

        public Boolean GoalAchieved
        {
            get
            {
                return _goalAchieved;
            }
            set
            {
                _goalAchieved = value;
            }
        }

        public void AutoMode(Object sender)
        {
            _dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { AutoModeInternal(sender); }));
        }
        private void AutoModeInternal(Object sensor)
        {
            if (sensor is ColorSensor)
            {
                ColorSensor s = (ColorSensor)sensor;

                TextBoxColorSensor.Text = s.ToString();
                if (s.ColorValue == 1) //<-- TODO : remplacer avec une ref qui pointe sur une ressource paramètrable
                {
                    GoalAchieved = true;
                    TextBoxConsole.Text += "\nGAME OVER -> Color on the ground Found !";
                    // stop the car here;
                }
            }
            else if (sensor is UltrasonicSensor)
            {
                UltrasonicSensor s = (UltrasonicSensor)sensor;
                
                // mettre algorithme de pilotage auto ici en fonction des objets détéctés.
                // if s.DistanceProximity == Distances.DANGER .....
            }
            
        }
    }
}
