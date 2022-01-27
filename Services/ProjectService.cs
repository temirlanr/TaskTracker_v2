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

        public void DeleteProject(int projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);

            if (project == null)
            {
                throw new ProjectNotFoundException("Project not found.");
            }

            _projectRepo.DeleteProject(project);
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

        public void UpdateProject(Project project)
        {
            _validation.ValidateProjectStatus(project.Status);
            _projectRepo.UpdateProject(project);
        }
    }
}
