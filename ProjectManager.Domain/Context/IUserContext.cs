using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Context
{
    public interface IUserContext
    {
        DbSet<UserEntity> Users { get; }
    }
}
