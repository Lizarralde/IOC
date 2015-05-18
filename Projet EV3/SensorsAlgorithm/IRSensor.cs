using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsAlgorithm
{
    public class IRSensor
    {
        private int _IRValue;

        // Getter/Setter
        public int IRValue
        {
            get
            {
                return _IRValue;
            }
            set
            {
                if (value >= 0)
                {
                    _IRValue = value;

                    if (OnIRValueChanged != null)
                        OnIRValueChanged(this);
                }
                else
                {
                    throw new InvalidIRValueException("Une distance ne peut pas être négative.");
                }
            }
        }

        public override string ToString()
        {
            return "[IRSensor] { _IRValue = " + IRValue + " }";
        }

        // Delegate
        public delegate void DelegateNotifyIRValue(Object sensor);
        public event DelegateNotifyIRValue OnIRValueChanged;
    }
}
