using System;

namespace ServerLibrary.Model
{
    public class ServerDBEntityException : Exception
    {
        public ServerDBEntityException()
        {
        }

        public ServerDBEntityException(string message) : base(message)
        {
        }
    }
}
