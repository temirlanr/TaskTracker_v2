using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Repositories;
using TaskTracker_v2.Validations;

namespace TaskTracker_v2.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepo _taskRepo;
        private readonly ITaskTrackerValidation _validation;

        public TaskService(ITaskRepo taskRepo, ITaskTrackerValidation validation)
        {
            _taskRepo = taskRepo;
            _validation = validation;
        }

        public void CreateTask(ProjectTask task)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(int taskId)
        {
            throw new NotImplementedException();
        }

        public ProjectTask GetTaskById(int taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectTask> GetTasks()
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(ProjectTask task)
        {
            throw new NotImplementedException();
        }
    }
}
