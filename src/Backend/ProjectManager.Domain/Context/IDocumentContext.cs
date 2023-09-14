using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Context
{
    public interface IDocumentContext
    {
        DbSet<TaskEntity> Tasks { get; }
    }
}
