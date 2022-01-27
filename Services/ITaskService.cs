using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Exceptions;

namespace TaskTracker_v2.Services
{
    public interface ITaskService
    {
        /// <summary>
        /// Gets the List of <see cref="ProjectTask"/>s
        /// </summary>
        /// <returns><see cref="ProjectTask"/> List</returns>
        IEnumerable<ProjectTask> GetTasks();
        /// <summary>
        /// Returns <see cref="ProjectTask"/> data given it Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns><see cref="ProjectTask"/></returns>
        /// <exception cref="TaskNotFoundException"></exception>
        ProjectTask GetTaskById(int taskId);
        /// <summary>
        /// Creates an instance of <see cref="ProjectTask"/> in database
        /// </summary>
        /// <param name="task"></param>
        /// <exception cref="InvalidStatusException"></exception>
        /// <exception cref="InvalidTaskIdException"></exception>
        void CreateTask(ProjectTask task);
        /// <summary>
        /// Updates <see cref="ProjectTask"/>
        /// </summary>
        /// <param name="task"></param>
        /// <exception cref="InvalidStatusException"></exception>
        void UpdateTask(ProjectTask task);
        /// <summary>
        /// Deletes <see cref="ProjectTask"/>
        /// </summary>
        /// <param name="taskId"></param>
        /// <exception cref="TaskNotFoundException"></exception>
        void DeleteTask(int taskId);
    }
}
