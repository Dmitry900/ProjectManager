using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Context
{
    public class ApplicationContext : DbContext, IBoardContext, IUserContext
    {
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
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }

        #endregion
    }
}