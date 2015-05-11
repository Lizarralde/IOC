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
        private ColorSensor colorSensor;
        private UltrasonicSensor ultrasonicSensor;
        private Adapter adapter;
        private Thread algoThread;
        private bool driving;
        private int colorTarget;

        private Mutex mut;

        public ColorSensor ColorSensor
        {
            get { return colorSensor; }
            set { colorSensor = value; }
        }
        public UltrasonicSensor UltrasonicSensor
        {
            get { return ultrasonicSensor; }
            set { ultrasonicSensor = value; }
        }

        public Mutex Mut
        {
            get { return mut; }
            set { mut = value; }
        }

        public bool Driving
        {
            get { return driving; }
            set { driving = value; }
        }

        public SensorControler(string connectionMethod)
        {
            ColorTarget = 1;
            colorSensor = new ColorSensor();
            ultrasonicSensor = new UltrasonicSensor();
            adapter = new Adapter(connectionMethod);
            mut = new Mutex();
        }

        public int ColorTarget
        {
            get { return colorTarget; }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    colorTarget = value;
                }
                else
                {
                    throw new InvalidColorValueException("Le numéro de la couleur doit être compris entre 0 et 7");
                }
            }
        }

        public void closeAdapter()
        {
            adapter.Close();
        }

        public void AbortThread()
        {
            algoThread.Abort();
            algoThread.Join();
            algoThread = null;
        }

        public void StopThread()
        {
            mut.WaitOne();
            Driving = false;
            mut.ReleaseMutex();
            if (algoThread != null)
            {
                algoThread.Join();
                algoThread = null;
            }
        }

        public void StartThread()
        {
            if (algoThread != null)
                return;
            algoThread = new Thread(this.CarDriving);
            algoThread.Start();
        }

        public void CarDriving()
        {
            mut.WaitOne();
            Driving = true;

            do
            {
                mut.ReleaseMutex();

                colorSensor.ColorValue = FormatColorValue(adapter.GetColorSensorValue());
                ultrasonicSensor.UltrasonicValue = FormatUltrasonicValue(adapter.GetUltrasonicSensorValue());

                if (colorSensor.ColorValue.Equals(ColorTarget))
                {
                    adapter.ControlCar((int)Directions.STOP, (sbyte)0);
                    Thread.Sleep(250);
                }
                else if (ultrasonicSensor.UltrasonicValue < 15)
                {
                    adapter.ControlCar((int)Directions.BACKWARD, (sbyte)100);
                    Thread.Sleep(750);
                    adapter.ControlCar((int)Directions.TURN_LEFT, (sbyte)65);
                    Thread.Sleep(2000);
                    adapter.ControlCar((int)Directions.FORWARD, (sbyte)65);
                    Thread.Sleep(250);
                }
                else
                {
                    adapter.ControlCar((int)Directions.FORWARD, (sbyte)65);
                    Thread.Sleep(250);
                }

                mut.WaitOne();
            } while (Driving);

            mut.ReleaseMutex();
            return;
        }

        private int FormatUltrasonicValue(string p)
        {
            return Int32.Parse(p);
        }

        private int FormatColorValue(string p)
        {
            return Int32.Parse(p);
        }

        public void ExecuteCommand(Directions directions)
        {
            switch (directions)
            {
                case Directions.BACKWARD:
                    adapter.ControlCar((int)directions, (sbyte)100);
                    break;
                case Directions.BACKWARD_LEFT:
                    adapter.ControlCar((int)directions, (sbyte)65);
                    break;
                case Directions.BACKWARD_RIGHT:
                    adapter.ControlCar((int)directions, (sbyte)65);
                    break;
                case Directions.FORWARD:
                    adapter.ControlCar((int)directions, (sbyte)65);
                    break;
                case Directions.FORWARD_LEFT:
                    adapter.ControlCar((int)directions, (sbyte)65);
                    break;
                case Directions.FORWARD_RIGHT:
                    adapter.ControlCar((int)directions, (sbyte)65);
                    break;
                case Directions.STOP:
                    adapter.ControlCar((int)directions, (sbyte)0);
                    break;
                case Directions.TURN_LEFT:
                    adapter.ControlCar((int)directions, (sbyte)50);
                    break;
                case Directions.TURN_RIGHT:
                    adapter.ControlCar((int)directions, (sbyte)50);
                    break;
            }
        }
    }
}
