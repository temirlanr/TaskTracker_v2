using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Exceptions;
using TaskTracker_v2.Extensions;
using TaskTracker_v2.Repositories;
using TaskTracker_v2.Validations;

namespace TaskTracker_v2.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepo _projectRepo;
        private readonly ITaskTrackerValidation _validation;

        public ProjectService(IProjectRepo projectRepo, ITaskTrackerValidation validation)
        {
            _projectRepo = projectRepo;
            _validation = validation;
        }

        public void CreateProject(Project project)
        {
            _validation.ValidateProjectStatus(project.Status);
            _projectRepo.CreateProject(project);
        }

        public void CreateTask(int projectId, ProjectTask task)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);

            if(existingProject == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            _validation.ValidateTaskStatus(task.Status);
            existingProject.Tasks.Add(task);
            _validation.ValidateTaskId(existingProject.Tasks);
            _projectRepo.UpdateProject(existingProject);
        }

        public void DeleteProject(int projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);

            if (project == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            _projectRepo.DeleteProject(project);
        }

        public void DeleteTask(int projectId, int taskId)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);
            if (existingProject == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            var existingTask = existingProject.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (existingTask == null)
            {
                throw new TaskNotFoundException("Task not found.");
            }

            existingProject.Tasks.Remove(existingTask);
            _projectRepo.UpdateProject(existingProject);
        }

        public Project GetProjectById(int projectId)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);
            if (existingProject == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            return existingProject;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _projectRepo.GetProjects();
        }

        public ProjectTask GetTaskById(int projectId, int taskId)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);
            if (existingProject == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            var existingTask = existingProject.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (existingTask == null)
            {
                throw new TaskNotFoundException("Task not found.");
            }

            return existingTask;
        }

        public IEnumerable<ProjectTask> GetTasks(int projectId)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);
            if (existingProject == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            return existingProject.Tasks;
        }

        public void UpdateProject(Project project)
        {
            _validation.ValidateProjectStatus(project.Status);
            _projectRepo.UpdateProject(project);
        }

        public void UpdateTask(int projectId, List<TaskUpdateOperation> taskUpdateOps)
        {
            var existingProject = _projectRepo.GetProjectById(projectId);
            if (existingProject == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            foreach(var op in taskUpdateOps)
            {
                var taskToUpdate = existingProject.Tasks.FirstOrDefault(t => t.Id == op.TaskId);
                if (taskToUpdate == null)
                {
                    throw new TaskNotFoundException("Task not found.");
                }

                switch (op.Property)
                {
                    case "Name":
                        taskToUpdate.Name = op.Value;
                        break;
                    case "Status":
                        _validation.ValidateTaskStatus(op.Value);
                        taskToUpdate.Status = op.Value;
                        break;
                    case "Description":
                        taskToUpdate.Description = op.Value;
                        break;
                    case "Priority":
                        taskToUpdate.Priority = Convert.ToInt32(op.Value);
                        break;
                    default:
                        throw new Exception($"Invalid Property name: {op.Property}.");
                }
            }
            _projectRepo.UpdateProject(existingProject);
        }
    }
}
