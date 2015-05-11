using MonoBrick.EV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WifiTransfer
{
    public class Adapter
    {
        private Brick<Sensor,Sensor,Sensor,Sensor> brick;
        
        public Adapter(string connectionMethod)
        {
            brick = new Brick<Sensor, Sensor, Sensor, Sensor>(connectionMethod);
            brick.Connection.Open();
            brick.Sensor1 = new IRSensor(IRMode.Proximity);
            brick.Sensor2 = new ColorSensor(ColorMode.Color);
            brick.Vehicle.LeftPort = MotorPort.OutA;
            brick.Vehicle.RightPort = MotorPort.OutD;
            brick.Vehicle.ReverseLeft = false;
            brick.Vehicle.ReverseRight = false;
        }

        public void ControlCar(int direction, sbyte speed)
        {
            switch (direction)
            {
                case 8:
                    brick.Vehicle.Forward(speed);
                    break;
                case 2:
                    brick.Vehicle.Backward(speed);
                    break;
                case 6:
                    brick.Vehicle.SpinRight(speed);
                    break;
                case 4:
                    brick.Vehicle.SpinLeft(speed);
                    break;
                case 7:
                    brick.Vehicle.TurnLeftForward(speed, 50);
                    break;
                case 9:
                    brick.Vehicle.TurnRightForward(speed, 50);
                    break;
                case 1:
                    brick.Vehicle.TurnRightReverse(speed, 50);
                    break;
                case 3:
                    brick.Vehicle.TurnLeftReverse(speed, 50);
                    break;
                case 0:
                    brick.Vehicle.Off();
                    break;
            }
        }

        public string GetColorSensorValue()
        {
            return brick.Sensor1.ReadAsString();
        }

        public string GetUltrasonicSensorValue()
        {
            return brick.Sensor2.ReadAsString();
        }

        public void Close()
        {
            brick.Connection.Close();
        }
    }
}
