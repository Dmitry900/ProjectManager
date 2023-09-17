using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface ITaskService
    {
        public Task CreateAsync(TaskModel task);
        public Task DeleteAsync(Guid taskId);
        public Task AddRecordsAsync(IEnumerable<TaskModel> tasks);

        public Task AddRecordAsync(TaskModel task);
        public Task RemoveRecordAsync(Guid recordId);
    }
}