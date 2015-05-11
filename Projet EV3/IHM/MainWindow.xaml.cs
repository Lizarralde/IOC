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

            _controler = new SensorControler("com8");

            _controler.ColorSensor.OnColorChanged += AutoMode;
            _controler.UltrasonicSensor.OnUltrasonicValueChanged += AutoMode;
            _dispatcher = Application.Current.Dispatcher;

            _autoModeIsOn = false;
            _goalAchieved = false;

            /*
            var binding = new Binding("ColorValue");
            binding.Source = TextBoxColorSensor;
            binding.Path = _controler.ColorSensor.
            //      binding.Path = new PropertyPath(ListBox.SelectedValueProperty);
            TextBoxColorSensor.SetBinding(TextBlock.TextProperty, binding);
             */
        }

        //--- CONTROL MODE METHODS
        private void RadioBoxAuto_Click(object sender, RoutedEventArgs e)
        {
            // Disable controls
            ButtonStart.IsEnabled = true;
            Button1.IsEnabled = false;
            Button2.IsEnabled = false;
            Button3.IsEnabled = false;
            Button4.IsEnabled = false;
            Button5.IsEnabled = false;
            Button6.IsEnabled = false;
            Button7.IsEnabled = false;
            Button8.IsEnabled = false;
            Button9.IsEnabled = false;

            // Tell the mindstorm auto mode is on by starting the thread
            _autoModeIsOn = true;
        }
        private void RadioBoxManual_Click(object sender, RoutedEventArgs e)
        {
            // Disable controls
            ButtonStart.IsEnabled = false;
            Button1.IsEnabled = true;
            Button2.IsEnabled = true;
            Button3.IsEnabled = true;
            Button4.IsEnabled = true;
            Button5.IsEnabled = true;
            Button6.IsEnabled = true;
            Button7.IsEnabled = true;
            Button8.IsEnabled = true;
            Button9.IsEnabled = true;

            // Tell the mindstorm manual mode is on by stopping the thread for the auto mode.     
            _autoModeIsOn = false;

            // Stop Car driving thread
            _controler.StopThread();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Equals(Button1)){
                //_controler.ExecuteCommand(Directions.BACKWARD_LEFT);
            }
            if (b.Equals(Button2))
            {
                //_controler.ExecuteCommand(Directions.BACKWARD);
            }
            if (b.Equals(Button3))
            {
                //_controler.ExecuteCommand(Directions.BACKWARD_RIGHT);
            }
            if (b.Equals(Button4))
            {
                //_controler.ExecuteCommand(Directions.TURN_LEFT);
            }
            if (b.Equals(Button5))
            {
               //_controler.ExecuteCommand(Directions.STOP);
            }
            if (b.Equals(Button6))
            {
               //_controler.ExecuteCommand(Directions.TURN_RIGHT);
            }
            if (b.Equals(Button7))
            {
               //_controler.ExecuteCommand(Directions.FORWARD_LEFT);
            }
            if (b.Equals(Button8))
            {
               //_controler.ExecuteCommand(Directions.FORWARD);
            }
            if (b.Equals(Button9))
            {
               //_controler.ExecuteCommand(Directions.FORWARD_RIGHT);
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                    //_controler.ExecuteCommand(Directions.BACKWARD_LEFT);
                    break;
                case Key.NumPad2:
                    //_controler.ExecuteCommand(Directions.BACKWARD);
                    break;
                case Key.NumPad3:
                    //_controler.ExecuteCommand(Directions.BACKWARD_RIGHT);
                    break;
                case Key.NumPad4:
                    //_controler.ExecuteCommand(Directions.TURN_LEFT);
                    break;
                case Key.NumPad5:
                    //_controler.ExecuteCommand(Directions.STOP);
                    break;
                case Key.NumPad6:
                    //_controler.ExecuteCommand(Directions.TURN_RIGHT);
                    break;
                case Key.NumPad7:
                    //_controler.ExecuteCommand(Directions.FORWARD_LEFT);
                    break;
                case Key.NumPad8:
                    //_controler.ExecuteCommand(Directions.FORWARD);
                    break;
                case Key.NumPad9:
                    //_controler.ExecuteCommand(Directions.FORWARD_RIGHT);
                    break;
                default:
                    break;
            }
        }
        
        //--- BUTTONS METHODS
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            _controler.StopThread();
            this.Close();
        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
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

        // Method for the delegate wich
        public void AutoMode(Object sender)
        {
            _dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { AutoModeInternal(sender); }));
        }

        /// <summary>
        /// Update the View.
        /// </summary>
        /// <param name="sensor"></param>
        private void AutoModeInternal(Object sensor)
        {
            if (sensor is ColorSensor)
            {
                ColorSensor s = (ColorSensor)sensor;
                PutMessageToConsole(s.ToString());
                TextBoxColorValue.Text = s.ColorValue.ToString();

                if (s.ColorValue == 1)
                {
                    GoalAchieved = true;
                    PutMessageToConsole("-----------------------------------------");
                    PutMessageToConsole("---------------- GAME OVER --------------");
                    PutMessageToConsole("-----------------------------------------");
                    PutMessageToConsole("Color " + s.GetColorName() + "(" + s.ColorValue + ")" + " Found");
                }
            }
            else if (sensor is UltrasonicSensor)
            {
                UltrasonicSensor s = (UltrasonicSensor)sensor;
                PutMessageToConsole(s.ToString());
            }
            
        }

        private void PutMessageToConsole(String message)
        {
            TextBoxConsole.Text += "\n" + message;
        }
    }
}
