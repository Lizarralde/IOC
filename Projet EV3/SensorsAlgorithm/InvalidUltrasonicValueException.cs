using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsAlgorithm
{
    public class InvalidUltrasonicValueException : Exception
    {
        public InvalidUltrasonicValueException(String message) : base(message) { }
    }
}
