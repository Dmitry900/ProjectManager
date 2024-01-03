using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface IBoardService
    {
        Task<BoardEntity> CreateAsync(Guid userId, string name, CancellationToken cancellationToken);
        Task<BoardEntity> UpdateAsync(BoardModel model, CancellationToken cancellationToken);
        Task<BoardEntity> FindAsync(Guid boardId, CancellationToken cancellationToken);
        Task<IEnumerable<TaskEntity>> GetAllTasksAsync(Guid boardId, CancellationToken cancellationToken);
        Task DeleteAsync(Guid boardId, CancellationToken cancellationToken);
        Task AddStatusAsync(Guid boardId, string status, CancellationToken cancellationToken);
        Task RemoveStatusAsync(Guid boardId, string status, CancellationToken cancellationToken);
    }
}