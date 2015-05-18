using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsAlgorithm
{
    /// <summary>
    /// Créer un object de la classe ColorSensor.
    /// </summary>
    public class ColorSensor
    {
        //--- PROPERTIES
        private int _colorValue;

        //--- GETTERS AND SETTERS

        /// <summary>
        /// Retourne
        /// </summary>
        public int ColorValue
        {
            get
            {
                return _colorValue;
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    _colorValue = value;
                    if (OnColorChanged != null)
                        OnColorChanged(this);
                }
                else
                {
                    throw new InvalidColorValueException("Le numéro de la couleur doit être compris entre 0 et 7");
                }
            }
        }

        /// <summary>
        /// Retourne la nom de la couleur correspondant à son numéro.
        /// </summary>
        /// <returns>Chaîne correspondant à la couleur.</returns>
        public String GetColorName()
        {
            String res = null;

            switch (ColorValue)
            {
                case 0:
                    res = "None";
                    break;
                case 1:
                    res = "Black";
                    break;
                case 2:
                    res = "Blue";
                    break;
                case 3:
                    res = "Green";
                    break;
                case 4:
                    res = "Yellow";
                    break;
                case 5:
                    res = "Red";
                    break;
                case 6:
                    res = "White";
                    break;
                case 7:
                    res = "Brown";
                    break;
                default:
                    res = "Color Error";
                    break;
            }

            return res;
        }


        /// <summary>
        /// Retourne une chaîne qui représente l'objet actif.
        /// </summary>
        /// <returns>Chaîne qui représente l'objet actif.</returns>
        public override string ToString()
        {
            return "[ColorSensor] { colorValue = " + ColorValue + ", colorName = " + GetColorName() + " }";
        }

        //--- NOTIFY A CHANGE OF COLOR
        public delegate void DelegateNotifyColor(Object sensor);
        public event DelegateNotifyColor OnColorChanged;
    }
}
