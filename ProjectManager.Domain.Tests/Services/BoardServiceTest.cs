using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;

namespace ProjectManager.Domain.Tests.Services
{
    public class BoardServiceTest : TestBase
    {
        readonly IBoardService boardService;
        readonly ITaskService taskService;
        public BoardServiceTest()
        {
            boardService = Services.GetRequiredService<IBoardService>();
            taskService = Services.GetRequiredService<ITaskService>();
        }

        [Fact]
        public async Task Create_Success()
        {
            var boardId = Guid.NewGuid();
            await boardService.CreateAsync(new(boardId, "abc"), NonToken);

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
            var task = await boardService.CreateAsync(new(boardId, "abc"), NonToken);

            await UnitOfWork.SaveAsync(NonToken);

            #endregion

            await boardService.DeleteAsync(task.BoardId, NonToken);
            var taskEntities = await UnitOfWork.TaskRepository.GetAsync();

            Assert.Empty(taskEntities);
        }

        [Fact]
        public async Task GetAll_Success()
        {
            var boardId = Guid.NewGuid();
            await taskService.CreateAsync(new(boardId, "abc"), NonToken);
            await taskService.CreateAsync(new(boardId, "abc1"), NonToken);

            await UnitOfWork.SaveAsync(NonToken);
            var taskEntities = await boardService.GetAllTasksAsync(boardId, NonToken);
            Assert.Collection(taskEntities, it =>
            {
                Assert.Equal(boardId, it.BoardId);
                Assert.Equal("abc", it.Name);
            }, it =>
            {
                Assert.Equal(boardId, it.BoardId);
                Assert.Equal("abc1", it.Name);
            });
        }

        [Fact]
        public async Task AddStatus_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            await taskService.CreateAsync(new(boardId, "abc"), NonToken);

            #endregion

            await boardService.AddStatusAsync(boardId, "new", NonToken);

            await UnitOfWork.SaveAsync(CancellationToken.None);
            var recordEntities = await UnitOfWork.BoardRepository.GetAsync();
            var recordEntity = recordEntities.Single();

            Assert.Single(recordEntity.Statuses);
            Assert.Equal("new", recordEntity.Statuses.Single());
        }

        [Fact]
        public async Task RemoveStatus_Success()
        {
            #region Preparation

            var boardId = Guid.NewGuid();
            await taskService.CreateAsync(new(boardId, "abc"), NonToken);


            await boardService.AddStatusAsync(boardId, "new", NonToken);

            #endregion
            await boardService.RemoveStatusAsync(boardId, "new", NonToken);

            await UnitOfWork.SaveAsync(CancellationToken.None);
            var recordEntities = await UnitOfWork.BoardRepository.GetAsync();
            Assert.Empty(recordEntities);
        }
    }
}