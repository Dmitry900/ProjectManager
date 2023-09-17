using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface IBoardService
    {
        Task CreateAsync(BoardModel board, CancellationToken cancellationToken);
        Task DeleteAsync(Guid boardId, CancellationToken cancellationToken);
        Task AddStatusAsync(Guid boardId, string status, CancellationToken cancellationToken);
        Task RemoveStatusAsync(Guid boardId, string status, CancellationToken cancellationToken);

        Task GetAllTasksAsync(Guid boardId, CancellationToken cancellationToken);
    }
}