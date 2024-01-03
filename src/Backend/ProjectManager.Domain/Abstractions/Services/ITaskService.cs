using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface ITaskService
    {
        public Task<TaskEntity> CreateAsync(Guid boardId, string name, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid taskId, CancellationToken cancellationToken);
        public Task<TaskEntity> FindAsync(Guid taskId, CancellationToken cancellationToken);
        public Task<RecordEntity> AddRecordAsync(Guid taskId, RecordType type, string text, CancellationToken cancellationToken);
        public Task RemoveRecordAsync(Guid recordId, CancellationToken cancellationToken);
    }
}