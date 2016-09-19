using System;

namespace ServerLibrary.Model
{
    public class ServerConflictException : Exception
    {
        public ServerConflictException()
        {
        }

        public ServerConflictException(string message) : base(message)
        {
        }
    }
}
