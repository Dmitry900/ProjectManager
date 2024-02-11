using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Context.Entities;

namespace ProjectManager.Core.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
