using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskly.Domain.Entities;

namespace Taskly.Infrastructure.Persistence
{
    public class TasklyDbContext : DbContext
    {
        public TasklyDbContext(DbContextOptions<TasklyDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(e =>
            {
                e.ToTable("TaskItems");
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).HasMaxLength(2000);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(x => x.Id);
                e.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            });
        }
    }
}
