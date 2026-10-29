using System;
using System.Linq;

namespace AttendEdgeWebConsole.Infrastructure
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message) { }
    }
}
