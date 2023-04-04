using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.BusinessLayer
{
    [Serializable]
    class DataDefinitionException : Exception 
    {
        public DataDefinitionException() { }

        public DataDefinitionException(string message)
            : base(message) { }

        public DataDefinitionException(string message, Exception inner)
            : base(message, inner) { }
    }
}
