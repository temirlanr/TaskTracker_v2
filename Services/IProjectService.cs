using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Extensions;
using TaskTracker_v2.Exceptions;

namespace TaskTracker_v2.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Gets the List of <see cref="Project"/>s
        /// </summary>
        /// <returns><see cref="Project"/> List</returns>
        IEnumerable<Project> GetProjects();
        /// <summary>
        /// Returns <see cref="Project"/> data given it Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns><see cref="Project"/></returns>
        /// <exception cref="ProjectNotFoundException"></exception>
        Project GetProjectById(int projectId);
        /// <summary>
        /// Filters <see cref="Project"/> List by the Id
        /// </summary>
        /// <remarks>
        /// Operator Options: 0 = Equal, 1 = Greater than or equal, 2 = Less than or equal;
        /// SortOrder Options: 0 = Ascending, 1 = Descending.
        /// </remarks>
        /// <param name="options"></param>
        /// <returns><see cref="Project"/> List</returns>
        IEnumerable<Project> FilterByProjectId(ProjectRetrievalOptions options);
        /// <summary>
        /// Creates an instance of <see cref="Project"/> in database
        /// </summary>
        /// <param name="project"></param>
        /// <exception cref="InvalidStatusException"></exception>
        void CreateProject(Project project);
        /// <summary>
        /// Updates <see cref="Project"/>
        /// </summary>
        /// <param name="project"></param>
        /// <exception cref="InvalidStatusException"></exception>
        void UpdateProject(Project project);
        /// <summary>
        /// Deletes <see cref="Project"/>
        /// </summary>
        /// <param name="projectId"></param>
        /// <exception cref="ProjectNotFoundException"></exception>
        void DeleteProject(int projectId);

    }
}
