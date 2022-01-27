using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker_v2.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException() { }
        public TaskNotFoundException(string message) : base(message) { }
        public TaskNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
