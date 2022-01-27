using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Repositories
{
    public interface IProjectRepo
    {
        IEnumerable<Project> GetProjects();
        Project GetProjectById(int id);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);
    }
}
