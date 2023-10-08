using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Services
{
    internal class TaskService : ITaskService
    {
        readonly DbSet<TaskEntity> tasks;
        readonly DbSet<RecordEntity> records;

        public TaskService(IBoardContext boardContext)
        {
            tasks = boardContext.Tasks ?? throw new ArgumentNullException(nameof(boardContext));
            records = boardContext.Records ?? throw new ArgumentNullException(nameof(boardContext));
        }

        #region ITaskService members

        public async Task CreateAsync(TaskModel task, CancellationToken cancellationToken)
        {
            var entity = new TaskEntity
            {
                TaskId = Guid.NewGuid()
            };
            await tasks.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = await GetTaskAsync(taskId, cancellationToken);
            tasks.Remove(task);
        }

        public async Task AddRecordAsync(Guid taskId, RecordModel record, CancellationToken cancellationToken)
        {
            var entity = new RecordEntity
            {
                RecordId = Guid.NewGuid(),
                TaskId = taskId,
                Text = record.Text,
                Type = record.Type
            };
            await records.AddAsync(entity, cancellationToken);
        }

        public async Task AddRecordsAsync(Guid taskId, IEnumerable<RecordModel> records, CancellationToken cancellationToken)
        {
            foreach (var record in records)
            {
                var entity = new RecordEntity
                {
                    RecordId = Guid.NewGuid(),
                    TaskId = taskId,
                    Text = record.Text,
                    Type = record.Type
                };
                await this.records.AddAsync(entity, cancellationToken);
            }
        }

        public async Task RemoveRecordAsync(Guid taskId, Guid recordId, CancellationToken cancellationToken)
        {
            var record = await GetRecordAsync(recordId, cancellationToken);
            records.Remove(record);
        }

        #endregion

        Task<TaskEntity> GetTaskAsync(Guid taskId, CancellationToken cancellationToken) => tasks.FirstOrDefaultAsync(it => it.TaskId == taskId, cancellationToken);
        Task<RecordEntity> GetRecordAsync(Guid recordId, CancellationToken cancellationToken) => records.FirstOrDefaultAsync(it => it.RecordId == recordId, cancellationToken);
    }
}
