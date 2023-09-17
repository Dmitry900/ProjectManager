using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Abstractions.Context
{
    public interface IUserContext
    {
        DbSet<UserEntity> Users { get; }
    }
}
