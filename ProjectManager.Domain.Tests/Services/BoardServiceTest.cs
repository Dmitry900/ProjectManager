using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;

namespace ProjectManager.Domain.Tests.Services
{
    public class BoardServiceTest : TestBase
    {
        readonly IUserService userService;
        readonly IBoardService boardService;
        readonly ITaskService taskService;
        public BoardServiceTest()
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

            #endregion

            var board = await boardService.CreateAsync(user.UserId, "Name", NonToken);

            await Context.SaveChangesAsync(NonToken);
            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);
            Assert.Equal(board.BoardId, boardEntity.BoardId);
            Assert.Equal("Name", boardEntity.Name);
        }

        [Fact]
        public async Task Update_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);
            var board = await boardService.CreateAsync(user.UserId, "Name", NonToken);

            await Context.SaveChangesAsync(NonToken);

            #endregion

            await boardService.UpdateNameAsync(board.BoardId, "newName", NonToken);

            await Context.SaveChangesAsync(NonToken);

            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);
            Assert.Equal(board.BoardId, boardEntity.BoardId);
            Assert.Equal("newName", boardEntity.Name);
        }

        [Fact]
        public async Task Delete_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);
            var board = await boardService.CreateAsync(user.UserId, "abc", NonToken);

            await Context.SaveChangesAsync(NonToken);

            #endregion

            await boardService.DeleteAsync(board.BoardId, NonToken);
            await Context.SaveChangesAsync(NonToken);

            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);

            Assert.Null(boardEntity);
        }

        [Fact]
        public async Task GetAll_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);

            var board = await boardService.CreateAsync(user.UserId, "abc", NonToken);

            #endregion

            var task1 = await taskService.CreateAsync(board.BoardId, "abc", NonToken);
            var task2 = await taskService.CreateAsync(board.BoardId, "abc1", NonToken);

            await Context.SaveChangesAsync(NonToken);
            var taskEntities = await boardService.GetAllTasksAsync(board.BoardId, NonToken);
            Assert.Collection(taskEntities, it =>
            {
                Assert.Equal(task1.BoardId, it.BoardId);
                Assert.Equal("abc", it.Name);
            }, it =>
            {
                Assert.Equal(task1.BoardId, it.BoardId);
                Assert.Equal("abc1", it.Name);
            });
        }

        [Fact]
        public async Task AddStatus_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);

            var board = await boardService.CreateAsync(user.UserId, "abc", NonToken);

            #endregion

            await boardService.AddStatusAsync(board.BoardId, "new", NonToken);

            await Context.SaveChangesAsync(CancellationToken.None);
            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);

            Assert.Single(boardEntity.Statuses);
            Assert.Equal("new", boardEntity.Statuses.Single());
        }

        [Fact]
        public async Task RemoveStatus_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);

            var board = await boardService.CreateAsync(user.UserId, "abc", NonToken);
            await boardService.AddStatusAsync(board.BoardId, "new", NonToken);

            await Context.SaveChangesAsync(CancellationToken.None);

            #endregion

            await boardService.RemoveStatusAsync(board.BoardId, "new", NonToken);

            await Context.SaveChangesAsync(CancellationToken.None);
            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);
            Assert.Empty(boardEntity.Statuses);
        }
    }
}