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
    public class ColorSensor : INotifyPropertyChanged
    {
        //--- PROPERTIES
        private int _colorValue;

        //--- CONSTRUCTORS
        /// <summary>
        /// Initialise un capteur avec aucune couleur.
        /// </summary>
        public ColorSensor()
        {
            ColorValue = 0;
        }

        /// <summary>
        /// Initialise un capteur avec une couleur donnée.
        /// </summary>
        /// <param name="colorValue">Valeur numérique de la couleur compris entre 0 et 7 inclus.</param>
        public ColorSensor(int colorValue)
        {
            ColorValue = colorValue;
        }

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
                    if(OnColorChanged != null)
                        OnColorChanged(this);
                    OnPropertyChanged("ColorValue");
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
            String res = "";
            switch (_colorValue)
            {
                case 0:
                    res = "Aucune";
                    break;
                case 1:
                    res = "Noir";
                    break;
                case 2:
                    res = "Bleu";
                    break;
                case 3:
                    res = "Vert";
                    break;
                case 4:
                    res = "Jaune";
                    break;
                case 5:
                    res = "Rouge";
                    break;
                case 6:
                    res = "Blanc";
                    break;
                case 7:
                    res = "Marron";
                    break;
                default:
                    res = "Erreur";
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
            return "ColorSensor { colorValue = " + ColorValue + " , colorName = " + GetColorName() + " }";
        }

        /// <summary>
        /// Détermine si l'objet spécifié est identique à l'objet actuel.
        /// </summary>
        /// <param name="obj">Objet à comparer avec l'objet actif. </param>
        /// <returns>true si l'objet spécifié est égal à l'objet actif ; sinon, false.</returns>
        public override bool Equals(Object obj)
        {
            ColorSensor o = (ColorSensor)obj;
            return ColorValue.Equals(o.ColorValue);
        }
        
        //--- NOTIFY A CHANGE OF COLOR
        public delegate void DelegateNotifyColor(Object sensor);
        public event DelegateNotifyColor OnColorChanged;

        
        //--- NOTIFY A PROPERTY CHANGED
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
