using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker_v2.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException() { }
        public InvalidStatusException(string message) : base(message) { }
        public InvalidStatusException(string message, Exception exception) : base(message, exception) { }
    }
}
