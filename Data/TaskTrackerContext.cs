using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Data
{
    public class TaskTrackerContext: DbContext
    {
        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure one-to-may relation
            modelBuilder.Entity<ProjectTask>().HasOne(p => p.Project).WithMany(t => t.Tasks).HasForeignKey(k => k.ProjectId);
        }
    }
}
