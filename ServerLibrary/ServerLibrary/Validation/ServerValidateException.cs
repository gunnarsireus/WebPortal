using System;

namespace ServerLibrary.Model
{
    public class ServerValidateException : Exception
    {
        public ServerValidateException()
        {
        }

        public ServerValidateException(string message) : base(message)
        {
        }
    }
}
