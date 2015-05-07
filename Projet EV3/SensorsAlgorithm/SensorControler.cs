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

            colorSensor = new ColorSensor();
            ultrasonicSensor = new UltrasonicSensor();
            adapter = new Adapter(connectionMethod);
            mut = new Mutex();
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
            Directions direction = Directions.STOP;
            Distances distance = Distances.SURE;
            sbyte speed = 50;

            do
            {
                mut.ReleaseMutex();

                colorSensor.ColorValue = FormatColorValue(adapter.GetColorSensorValue());
                //ultrasonicSensor.UltrasonicValue = FormatUltrasonicValue(adapter.GetUltrasonicSensorValue());

                // ********************
                // TODO : ALGO ICI
                // ********************

                adapter.ControlCar((int)direction, speed);

                Thread.Sleep(250);
                mut.WaitOne();
            } while (Driving);

            mut.ReleaseMutex();
            return;
        }

        private int FormatUltrasonicValue(string p)
        {
            throw new NotImplementedException();
        }

        private int FormatColorValue(string p)
        {
            // TODO : implemente better method
            //throw new NotImplementedException();
            return Int32.Parse(p);
        }
    }
}
