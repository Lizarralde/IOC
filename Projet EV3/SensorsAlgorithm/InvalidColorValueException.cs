using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorsAlgorithm
{
    public class InvalidColorValueException : Exception
    {
        public InvalidColorValueException(String message) : base(message) { }
    }
}
