using System;

namespace ServerLibrary.Model
{
    public class ServerAuthorizeException : Exception
    {
        public ServerAuthorizeException()
        {
        }

        public ServerAuthorizeException(string message) : base(message)
        {
        }
    }
}
