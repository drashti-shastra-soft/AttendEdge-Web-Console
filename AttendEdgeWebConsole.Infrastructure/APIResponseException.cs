using System;

namespace AttendEdgeWebConsole.Infrastructure
{
    public class APIResponseException : Exception
    {
        public APIResponseException(string error) : base(error)
        {

        }
    }
}
