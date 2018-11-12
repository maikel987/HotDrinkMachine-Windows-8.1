using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDrinkMachine
{
    public class RunOutException : Exception
    {
        
        public RunOutException() { }
        public RunOutException(string message) : base( message ) { }
        public RunOutException(string message, Exception inner) : base( message, inner ) { }


    }
}
