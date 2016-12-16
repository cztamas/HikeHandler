using System;

namespace HikeHandler.Exceptions
{
    public class DBErrorException : Exception
    {
        public DBErrorException(string message) : base(message)
        { }
    }
}
