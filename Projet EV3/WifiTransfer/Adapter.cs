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
        // Déclaration de la brick
        private Brick<Sensor, Sensor, Sensor, Sensor> _brick;

        // Getter/setter
        public Brick<Sensor, Sensor, Sensor, Sensor> Brick
        {
            get { return _brick; }
            set { _brick = value; }
        }

        //Constructeur par défaut
        public Adapter(string link)
        {
            Brick = new Brick<Sensor, Sensor, Sensor, Sensor>(link);
            Brick.Connection.Open();

            // Déclaration des capteurs
            Brick.Sensor1 = new IRSensor(IRMode.Proximity);
            Brick.Sensor4 = new ColorSensor(ColorMode.Color);

            // Déclaration des moteurs
            Brick.Vehicle.LeftPort = MotorPort.OutA;
            Brick.Vehicle.RightPort = MotorPort.OutD;
            Brick.Vehicle.ReverseLeft = false;
            Brick.Vehicle.ReverseRight = false;
        }

        // Permet de déplacer le robot dans la direction souhaitée
        public void ControlCar(int direction, sbyte speed, sbyte angle)
        {
            switch (direction)
            {
                case 8:
                    Brick.Vehicle.Forward(speed);
                    break;
                case 2:
                    Brick.Vehicle.Backward(speed);
                    break;
                case 6:
                    Brick.Vehicle.SpinRight(speed);
                    break;
                case 4:
                    Brick.Vehicle.SpinLeft(speed);
                    break;
                case 7:
                    Brick.Vehicle.TurnLeftForward(speed, angle);
                    break;
                case 9:
                    Brick.Vehicle.TurnRightForward(speed, angle);
                    break;
                case 1:
                    Brick.Vehicle.TurnRightReverse(speed, angle);
                    break;
                case 3:
                    Brick.Vehicle.TurnLeftReverse(speed, angle);
                    break;
                case 5:
                    Brick.Vehicle.Off();
                    break;
            }
        }

        // Retourne la valeur du capteur colorimétrique
        public int GetColorSensorValue()
        {
            return (int)((ColorSensor)Brick.Sensor4).ReadColor();
        }

        // Retourne la valeur de l'IR
        public string GetIRSensorValue()
        {
            return ((IRSensor)Brick.Sensor1).ReadAsString();
        }

        // Ferme la connexion à la brick
        public void CloseConnection()
        {
            Brick.Connection.Close();
        }
    }
}
