using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Data;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Repositories
{
    public class TaskRepo : ITaskRepo
    {
        private readonly TaskTrackerContext _context;
        public TaskRepo(TaskTrackerContext context)
        {
            _context = context;
        }

        public void CreateTask(ProjectTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void DeleteTask(ProjectTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public ProjectTask GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<ProjectTask> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        public void UpdateTask(ProjectTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
    }
}
