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
        private readonly List<string> projectStatus = new() { "notstarted", "active", "completed" };
        private readonly List<string> taskStatus = new() { "todo", "inprogress", "done" };

        public void ValidateProjectStatus(string status)
        {
            if (!projectStatus.Contains(status.ToLower()))
            {
                throw new InvalidStatusException("Invalid project status.");
            }
        }

        public void ValidateTaskStatus(string status)
        {
            if (!taskStatus.Contains(status.ToLower()))
            {
                throw new InvalidStatusException("Invalid task status.");
            }
        }

        public void ValidateTaskId(List<ProjectTask> tasks)
        {
            var temp = new List<int>();
            foreach(var task in tasks)
            {
                if (temp.Contains(task.Id))
                {
                    throw new InvalidTaskIdException("Task ID(-s) not unique.");
                }
                else
                {
                    temp.Add(task.Id);
                }
            }
        }
    }
}
