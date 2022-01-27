using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Validations
{
    public interface ITaskTrackerValidation
    {
        void ValidateProjectStatus(string projectStatus);
        void ValidateTaskStatus(string taskStatus);
    }
}
