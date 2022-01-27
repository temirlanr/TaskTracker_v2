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

        public IEnumerable<Project> FilterByProjectId(ProjectRetrievalOptions options)
        {
            var projects = _projectRepo.GetProjects();

            switch (options.Filter.Operator)
            {
                case FilterOperator.EQ:
                    projects = projects.Where(p => p.Id == Int32.Parse(options.Filter.Value));
                    break;
                case FilterOperator.GTE:
                    projects = projects.Where(p => p.Id >= Int32.Parse(options.Filter.Value));
                    break;
                case FilterOperator.LTE:
                    projects = projects.Where(p => p.Id <= Int32.Parse(options.Filter.Value));
                    break;
                default:
                    throw new Exception("Given filter operator is not supported by property Id");
            }

            if (options.SortOrder == SortOrder.ASC)
            {
                projects = projects.OrderBy(p => p.Id);
            }
            else if (options.SortOrder == SortOrder.DESC)
            {
                projects = projects.OrderByDescending(p => p.Id);
            }

            return projects.ToList();
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
