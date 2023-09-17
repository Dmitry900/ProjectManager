using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Abstractions.Context
{
    public interface IBoardContext
    {
        DbSet<BoardEntity> Boards { get; }
        DbSet<TaskEntity> Tasks { get; }
        DbSet<RecordEntity> Records { get; }
    }
}
