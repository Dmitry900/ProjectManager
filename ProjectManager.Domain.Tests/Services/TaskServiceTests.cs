using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;

namespace ProjectManager.Domain.Tests.Services
{
    public class TaskServiceTests : TestBase
    {
        readonly IUserService userService;
        readonly IBoardService boardService;
        readonly ITaskService taskService;
        public TaskServiceTests()
        {
            userService = Services.GetRequiredService<IUserService>();
            boardService = Services.GetRequiredService<IBoardService>();
            taskService = Services.GetRequiredService<ITaskService>();
        }

        [Fact]
        public async Task Create_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);
            var board = await boardService.CreateAsync(user.UserId, "Name", NonToken);

            #endregion

            var task = await taskService.CreateAsync(board.BoardId, "abc", NonToken);

            await Context.SaveChangesAsync(NonToken);
            var taskEntity = await taskService.FindAsync(task.TaskId, NonToken);
            Assert.NotNull(taskEntity);
            Assert.Equal(task.BoardId, taskEntity.BoardId);
            Assert.Equal("abc", taskEntity.Name);
        }

        [Fact]
        public async Task Delete_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(boardId, "abc", NonToken);

            #endregion

            await taskService.DeleteAsync(task.TaskId, NonToken);
            var taskEntity = await taskService.FindAsync(task.TaskId, NonToken);

            await Context.SaveChangesAsync(NonToken);
            Assert.Null(taskEntity);
        }

        [Fact]
        public async Task AddRecord_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(boardId, "abc", NonToken);

            #endregion

            await taskService.AddRecordAsync(task.TaskId, Entities.RecordType.Text, "Abc", NonToken);

            await Context.SaveChangesAsync(CancellationToken.None);
            var taskEntity = await taskService.FindAsync(task.TaskId, NonToken);
            Assert.Single(taskEntity.Records);
            var recordEntity = taskEntity.Records.Single();
            Assert.Equal(task.TaskId, recordEntity.TaskId);
            Assert.Equal("Abc", recordEntity.Text);
        }

        [Fact]
        public async Task RemoveRecord_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            var task = await taskService.CreateAsync(boardId, "abc", NonToken);
            var recordEntity = await taskService.AddRecordAsync(task.TaskId, Entities.RecordType.Text, "Abc", NonToken);

            #endregion

            await taskService.RemoveRecordAsync(recordEntity.RecordId, NonToken);

            await Context.SaveChangesAsync(CancellationToken.None);
            var taskEntity = await taskService.FindAsync(task.TaskId, NonToken);
            Assert.Empty(taskEntity.Records);
        }
    }
}