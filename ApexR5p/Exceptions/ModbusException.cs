using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleCommunicator.Exceptions
{
    public class ModbusException : Exception
    {
        public ModbusException()
        {
        }
        public ModbusException(string message)
       : base(message)
        {
        }

        public ModbusException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
