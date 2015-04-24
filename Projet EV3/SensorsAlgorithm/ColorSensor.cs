using System;
using System.Collections.Generic;
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
        private int colorValue;

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
        public int ColorValue
        {
            get
            {
                return colorValue;
            }
            set
            {
                if (value >= 0 && value <= 7)
                {
                    colorValue = value;
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
            switch (colorValue)
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
    }
}
