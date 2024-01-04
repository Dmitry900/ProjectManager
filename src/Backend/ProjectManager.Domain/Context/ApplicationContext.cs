using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Context
{
    public class ApplicationContext : DbContext, IBoardContext, IUserContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #region IDocumentContext members

        public DbSet<BoardEntity> Boards { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<RecordEntity> Records { get; set; }

        #endregion

        #region IUserContext members

        public DbSet<UserEntity> Users { get; set; }

        #endregion

        #region DbContext

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(e => e.UserId);
            modelBuilder.Entity<UserEntity>().HasMany(e => e.Boards).WithOne().HasForeignKey(e => e.UserId);

            modelBuilder.Entity<BoardEntity>().HasKey(e => e.BoardId);
            modelBuilder.Entity<BoardEntity>().HasMany(e => e.Tasks).WithOne().HasForeignKey(e => e.BoardId).IsRequired();

            modelBuilder.Entity<TaskEntity>().HasKey(e => e.TaskId);
            modelBuilder.Entity<TaskEntity>().HasMany(e => e.Records).WithOne().HasForeignKey(e => e.TaskId).IsRequired();

            modelBuilder.Entity<RecordEntity>().HasKey(e => e.RecordId);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}