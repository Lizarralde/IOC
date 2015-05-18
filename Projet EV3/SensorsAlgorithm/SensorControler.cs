using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiTransfer;

namespace SensorsAlgorithm
{
    public class SensorControler
    {
        private bool _driving;
        private int _colorTarget;

        private Adapter _adapter;
        private ColorSensor _colorSensor;
        private IRSensor _IRSensor;

        private Thread _algorithm;
        private Mutex _mutex;

        // Getter/Setter
        public bool Driving
        {
            get { return _driving; }
            set { _driving = value; }
        }

        public int ColorTarget
        {
            get { return _colorTarget; }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _colorTarget = value;
                }
                else
                {
                    throw new InvalidColorValueException("Le numéro de la couleur doit être compris entre 0 et 7");
                }
            }
        }

        public Adapter Adapter
        {
            get { return _adapter; }
            set { _adapter = value; }
        }

        public ColorSensor ColorSensor
        {
            get { return _colorSensor; }
            set { _colorSensor = value; }
        }

        public IRSensor IRSensor
        {
            get { return _IRSensor; }
            set { _IRSensor = value; }
        }

        public Thread Algorithm
        {
            get { return _algorithm; }
            set { _algorithm = value; }
        }

        public Mutex Mutex
        {
            get { return _mutex; }
            set { _mutex = value; }
        }

        // Constructeur par défaut
        public SensorControler(string link)
        {
            ColorSensor = new ColorSensor();
            IRSensor = new IRSensor();
            Adapter = new Adapter(link);
            Mutex = new Mutex();
        }

        // Fermeture de la connexion à la brick
        public void closeAdapter()
        {
            Adapter.CloseConnection();
        }

        // Arrêt du thread de pilotage automatique
        public void StopThread()
        {
            Mutex.WaitOne();

            Driving = false;

            Mutex.ReleaseMutex();

            if (Algorithm != null)
            {
                Algorithm.Join();
                Algorithm = null;
            }
        }

        // Départ du thread de pilotage automatique
        public void StartThread()
        {
            if (Algorithm != null)
                return;

            Algorithm = new Thread(this.CarDriving);
            Algorithm.Start();
        }

        // Algorithme de pilotage automatique
        public void CarDriving()
        {
            Mutex.WaitOne();

            Driving = true;

            do
            {
                Mutex.ReleaseMutex();

                ColorSensor.ColorValue = Adapter.GetColorSensorValue();
                IRSensor.IRValue = Int32.Parse(Adapter.GetIRSensorValue());

                if (ColorSensor.ColorValue.Equals(ColorTarget))
                {
                    Adapter.ControlCar((int)Directions.STOP, (sbyte)0, (sbyte)0);
                    Thread.Sleep(250);
                }
                else if (IRSensor.IRValue < 15)
                {
                    Adapter.ControlCar((int)Directions.BACKWARD, (sbyte)50, (sbyte)0);
                    Thread.Sleep(250);
                    Adapter.ControlCar((int)Directions.TURN_LEFT, (sbyte)50, (sbyte)0);
                    Thread.Sleep(250);
                    Adapter.ControlCar((int)Directions.FORWARD, (sbyte)50, (sbyte)0);
                    Thread.Sleep(250);
                }
                else
                {
                    Adapter.ControlCar((int)Directions.FORWARD, (sbyte)50, (sbyte)0);
                    Thread.Sleep(250);
                }

                Mutex.WaitOne();
            } while (Driving);

            Mutex.ReleaseMutex();

            return;
        }

        // Permet le pilotage manuel du robot
        public void ExecuteCommand(Directions directions)
        {
            switch (directions)
            {
                case Directions.BACKWARD:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)0);
                    break;
                case Directions.BACKWARD_LEFT:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)50);
                    break;
                case Directions.BACKWARD_RIGHT:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)50);
                    break;
                case Directions.FORWARD:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)0);
                    break;
                case Directions.FORWARD_LEFT:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)50);
                    break;
                case Directions.FORWARD_RIGHT:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)50);
                    break;
                case Directions.STOP:
                    _adapter.ControlCar((int)directions, (sbyte)0, (sbyte)0);
                    break;
                case Directions.TURN_LEFT:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)0);
                    break;
                case Directions.TURN_RIGHT:
                    _adapter.ControlCar((int)directions, (sbyte)50, (sbyte)0);
                    break;
                default:
                    break;
            }
        }
    }
}
