using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Exceptions;
using TaskTracker_v2.Repositories;
using TaskTracker_v2.Validations;

namespace TaskTracker_v2.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepo _taskRepo;
        private readonly IProjectRepo _projectRepo;
        private readonly ITaskTrackerValidation _validation;

        public TaskService(ITaskRepo taskRepo, IProjectRepo projectRepo, ITaskTrackerValidation validation)
        {
            _taskRepo = taskRepo;
            _validation = validation;
            _projectRepo = projectRepo;
        }

        public void CreateTask(ProjectTask task)
        {
            _validation.ValidateTaskStatus(task.Status);
            _taskRepo.CreateTask(task);
        }

        public void DeleteTask(int taskId)
        {
            var task = _taskRepo.GetTaskById(taskId);

            if(task == null)
            {
                throw new TaskNotFoundException($"Task with Id {taskId} doesn't exist.");
            }

            _taskRepo.DeleteTask(task);
        }

        public ProjectTask GetTaskById(int taskId)
        {
            var task = _taskRepo.GetTaskById(taskId);

            if (task == null)
            {
                throw new TaskNotFoundException($"Task with Id {taskId} doesn't exist.");
            }

            return task;
        }

        public IEnumerable<ProjectTask> GetTasks()
        {
            return _taskRepo.GetTasks();
        }

        public IEnumerable<ProjectTask> GetTasksByProjectId(int projectId)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);

            if (existingProject == null)
            {
                throw new ProjectNotFoundException($"Project with Id {projectId} doesn't exist.");
            }

            return _taskRepo.GetTasks().Where(t => t.ProjectId == projectId);
        }

        public void UpdateTask(ProjectTask task)
        {
            _validation.ValidateTaskStatus(task.Status);
            _taskRepo.UpdateTask(task);
        }
    }
}
