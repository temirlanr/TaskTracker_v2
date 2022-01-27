using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Repositories
{
    public interface ITaskRepo
    {
        IEnumerable<ProjectTask> GetTasks();
        ProjectTask GetTaskById(int id);
        void CreateTask(ProjectTask task);
        void UpdateTask(ProjectTask task);
        void DeleteTask(ProjectTask task);
    }
}
