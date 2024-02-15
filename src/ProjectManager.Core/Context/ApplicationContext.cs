using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Context.Entities;

namespace ProjectManager.Core.Context
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasKey(it => it.Id);
            modelBuilder.Entity<Employee>().HasKey(it => it.Id);

            modelBuilder.Entity<Project>().HasMany(p => p.Employees).WithMany(it => it.Projects);
            modelBuilder.Entity<Project>().HasOne(p => p.Director);

            base.OnModelCreating(modelBuilder);
        }
    }
}
