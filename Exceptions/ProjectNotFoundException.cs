using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker_v2.Exceptions
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException() { }
        public ProjectNotFoundException(string message) : base(message) { }
        public ProjectNotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
