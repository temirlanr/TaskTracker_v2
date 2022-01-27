using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Data;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Repositories
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly TaskTrackerContext _context;
        public ProjectRepo(TaskTrackerContext context)
        {
            _context = context;
        }

        public void CreateProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void DeleteProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        public void UpdateProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            _context.Projects.Update(project);
            _context.SaveChanges();
        }
    }
}
