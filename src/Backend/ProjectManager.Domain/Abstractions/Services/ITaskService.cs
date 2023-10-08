using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface ITaskService
    {
        public Task CreateAsync(TaskModel task, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid taskId, CancellationToken cancellationToken);
        public Task AddRecordsAsync(Guid taskId, IEnumerable<RecordModel> records, CancellationToken cancellationToken);
        public Task AddRecordAsync(Guid taskId, RecordModel record, CancellationToken cancellationToken);
        public Task RemoveRecordAsync(Guid taskId, Guid recordId, CancellationToken cancellationToken);
    }
}