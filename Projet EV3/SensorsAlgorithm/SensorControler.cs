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
            algoThread.Join();
            algoThread = null;
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

            adapter.ControlCar((int)Directions.FORWARD, (sbyte)65);
            Thread.Sleep(250);

            do
            {
                mut.ReleaseMutex();

                colorSensor.ColorValue = FormatColorValue(adapter.GetColorSensorValue());
                ultrasonicSensor.UltrasonicValue = FormatUltrasonicValue(adapter.GetUltrasonicSensorValue());

                if (!colorSensor.ColorValue.Equals(ColorTarget))
                {
                    adapter.ControlCar((int)Directions.BACKWARD, (sbyte)100);
                    Thread.Sleep(750);
                    adapter.ControlCar((int)Directions.TURN_LEFT, (sbyte)75);
                    Thread.Sleep(2000);
                    adapter.ControlCar((int)Directions.FORWARD, (sbyte)65);
                    Thread.Sleep(250);
                }
                else
                {
                    adapter.ControlCar((int)Directions.STOP, (sbyte)0);
                    Thread.Sleep(250);
                }

                mut.WaitOne();
            } while (Driving);

            mut.ReleaseMutex();
            return;
        }

        private int FormatUltrasonicValue(string p)
        {
            return 20;
        }

        private int FormatColorValue(string p)
        {
            // TODO : implemente better method
            //throw new NotImplementedException();
            return Int32.Parse(p);
        }
    }
}
