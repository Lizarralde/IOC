using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsAlgorithm
{
    public class UltrasonicSensor
    {
        private int _ultrasonicValue;

        // Constructeur par défaut
        public UltrasonicSensor()
        {
            UltrasonicValue = 0;
        }

        public UltrasonicSensor(int ultrasonicValue)
        {
            UltrasonicValue = ultrasonicValue;
        }

        // Getter/Setter
        public int UltrasonicValue
        {
            get
            {
                return _ultrasonicValue;
            }
            set
            {
                if (value >= 0)
                {
                    _ultrasonicValue = value;

                    if (OnUltrasonicValueChanged != null)
                        OnUltrasonicValueChanged(this);

                    OnPropertyChanged("UltrasonicValue");
                }
                else
                {
                    throw new InvalidUltrasonicValueException("Une distance ne peut pas être négative.");
                }
            }
        }

        public override string ToString()
        {
            return "UltrasonicSensor { _ultrasonicValue = " + UltrasonicValue + " }";
        }

        public override bool Equals(Object obj)
        {
            UltrasonicSensor o = (UltrasonicSensor)obj;
            return UltrasonicValue.Equals(o.UltrasonicValue);
        }

        // Delegate
        public delegate void DelegateNotifyUltrasonicValue(Object sensor);
        public event DelegateNotifyUltrasonicValue OnUltrasonicValueChanged;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
