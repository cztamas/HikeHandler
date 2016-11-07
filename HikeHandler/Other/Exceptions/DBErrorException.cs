using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HikeHandler.Exceptions
{
    class DBErrorException : Exception
    {
        public DBErrorException(string message) : base(message)
        { }
    }
}
