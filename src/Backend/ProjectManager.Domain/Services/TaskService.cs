﻿using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Domain.Services
{
    internal class TaskService(IUnitOfWork unitOfWork) : ITaskService
    {
        readonly IGenericRepository<TaskEntity> taskRepository = unitOfWork.TaskRepository ?? throw new ArgumentNullException(nameof(unitOfWork));
        readonly IGenericRepository<RecordEntity> recordRepository = unitOfWork.RecordRepository ?? throw new ArgumentNullException(nameof(unitOfWork));

        #region ITaskService members

        public async Task<TaskEntity> CreateAsync(TaskModel task, CancellationToken cancellationToken)
        {
            var entity = new TaskEntity
            {
                TaskId = Guid.NewGuid(),
                BoardId = task.BoardId,
                Name = task.Name
            };
            await taskRepository.InsertAsync(entity, cancellationToken);

            return entity;
        }

        public Task DeleteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            return taskRepository.DeleteAsync(taskId, cancellationToken);
        }

        public async Task<RecordEntity> AddRecordAsync(Guid taskId, RecordModel record, CancellationToken cancellationToken)
        {
            var entity = new RecordEntity
            {
                RecordId = Guid.NewGuid(),
                TaskId = taskId,
                Text = record.Text,
                Type = record.Type
            };
            await recordRepository.InsertAsync(entity, cancellationToken);

            return entity;
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

                await recordRepository.InsertAsync(entity, cancellationToken);
            }
        }

        public Task RemoveRecordAsync(Guid taskId, Guid recordId, CancellationToken cancellationToken)
        {
            return recordRepository.DeleteAsync(recordId, cancellationToken);
        }

        #endregion
    }
}
