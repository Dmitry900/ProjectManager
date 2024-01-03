using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Services
{
    internal class TaskService(IBoardContext boardContext) : ITaskService
    {
        readonly IBoardContext boardContext = boardContext ?? throw new ArgumentNullException(nameof(boardContext));

        #region ITaskService members

        public async Task<TaskEntity> CreateAsync(Guid boardId, string name, CancellationToken cancellationToken)
        {
            var entity = new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                BoardId = boardId,
                Name = name
            };
            await boardContext.Tasks.AddAsync(entity, cancellationToken);

            return entity;
        }

        // todo: дополнить и реализовать интерфейсы
        public Task<TaskEntity> FindAsync(Guid taskId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = await boardContext.Tasks.FindAsync(taskId, cancellationToken);
            boardContext.Tasks.Remove(task);
        }

        public async Task<RecordEntity> AddRecordAsync(Guid taskId, RecordType type, string text, CancellationToken cancellationToken)
        {
            var entity = new RecordEntity
            {
                RecordId = Guid.NewGuid(),
                TaskId = taskId,
                Text = text,
                Type = type
            };
            await boardContext.Records.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task RemoveRecordAsync(Guid recordId, CancellationToken cancellationToken)
        {
            var records = await boardContext.Records.FindAsync(recordId, cancellationToken);
            boardContext.Records.Remove(records);
        }

        #endregion
    }
}
