using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Tests.Services
{
    public class TaskServiceTests : TestBase
    {
        readonly ITaskService taskService;
        public TaskServiceTests()
        {
            taskService = Services.GetRequiredService<ITaskService>();
        }

        [Fact]
        public async Task Create_Success()
        {
            var boardId = Guid.NewGuid();
            await taskService.CreateAsync(new(boardId, "abc"), NonToken);

            await UnitOfWork.SaveAsync(NonToken);
            var taskEntities = await UnitOfWork.TaskRepository.GetAsync();
            Assert.Single(taskEntities);
            var task = taskEntities.Single();
            Assert.Equal(boardId, task.BoardId);
            Assert.Equal("123", task.Name);
        }

        [Fact]
        public async Task Delete_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(new(boardId, "abc"), NonToken);

            await UnitOfWork.SaveAsync(NonToken);

            #endregion

            await taskService.DeleteAsync(task.TaskId, NonToken);
            var taskEntities = await UnitOfWork.TaskRepository.GetAsync();

            Assert.Empty(taskEntities);
        }

        [Fact]
        public async Task AddRecord_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(new(boardId, "abc"), NonToken);

            #endregion

            var recordEntity = await taskService.AddRecordAsync(task.TaskId, new RecordModel(Entities.RecordType.Text, "Abc"), NonToken);

            await UnitOfWork.SaveAsync(CancellationToken.None);
            var recordEntities = await UnitOfWork.RecordRepository.GetAsync();
            Assert.Single(recordEntities);
            var record = recordEntities.Single();
            Assert.Equal(task.TaskId, record.TaskId);
            Assert.Equal("Abc", record.Text);
        }

        [Fact]
        public async Task AddRecords_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(new(boardId, "abc"), NonToken);

            #endregion

            var record1 = new RecordModel(Entities.RecordType.Text, "Abc");
            var record2 = new RecordModel(Entities.RecordType.Text, "Cba");

            await taskService.AddRecordsAsync(task.TaskId, new List<RecordModel> { record1, record2 }, NonToken);

            await UnitOfWork.SaveAsync(CancellationToken.None);
            var recordEntities = await UnitOfWork.RecordRepository.GetAsync();
            var recordEntity1 = recordEntities.First();
            var recordEntity2 = recordEntities.Last();

            Assert.Equal(task.TaskId, recordEntity1.TaskId);
            Assert.Equal("Abc", recordEntity1.Text);

            Assert.Equal(task.TaskId, recordEntity2.TaskId);
            Assert.Equal("Cba", recordEntity2.Text);
        }

        [Fact]
        public async Task RemoveRecord_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(new(boardId, "abc"), NonToken);
            var recordEntity = await taskService.AddRecordAsync(task.TaskId, new RecordModel(Entities.RecordType.Text, "Abc"), NonToken);

            #endregion

            await taskService.RemoveRecordAsync(task.TaskId, recordEntity.RecordId, NonToken);

            await UnitOfWork.SaveAsync(CancellationToken.None);
            var recordsEntities = await UnitOfWork.RecordRepository.GetAsync();
            Assert.Empty(recordsEntities);
        }
    }
}