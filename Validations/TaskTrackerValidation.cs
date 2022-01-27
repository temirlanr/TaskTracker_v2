using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Exceptions;

namespace TaskTracker_v2.Validations
{
    public class TaskTrackerValidation : ITaskTrackerValidation
    {
        private readonly List<string> projectStatus = new() { "NotStarted", "Active", "Completed" };
        private readonly List<string> taskStatus = new() { "ToDo", "InProgress", "Done" };

        public void ValidateProjectStatus(string status)
        {
            if (!projectStatus.Contains(status))
            {
                throw new InvalidStatusException("Invalid project status.");
            }
        }

        public void ValidateTaskStatus(string status)
        {
            if (!taskStatus.Contains(status))
            {
                throw new InvalidStatusException("Invalid task status.");
            }
        }
    }
}
