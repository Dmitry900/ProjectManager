using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Context
{
    public class AppContext : DbContext, IDocumentContext, IUserContext
    {
        #region IDocumentContext members

        public DbSet<TaskEntity> Tasks { get; set; }

        #endregion

        #region IUserContext members

        public DbSet<UserEntity> Users { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
        }
    }
}
