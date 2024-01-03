using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;

namespace ProjectManager.Domain.Tests.Services
{
    public class BoardServiceTest : TestBase
    {
        readonly IUserService userService;
        readonly IBoardService boardService;
        public BoardServiceTest()
        {
            userService = Services.GetRequiredService<IUserService>();
            boardService = Services.GetRequiredService<IBoardService>();
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
        public async Task Delete_Success()
        {
            #region Preparation

            var user = await userService.CreateUserAsync("abc", "123", NonToken);
            var board = await boardService.CreateAsync(user.UserId, "abc", NonToken);

            await Context.SaveChangesAsync(NonToken);

            #endregion

            await boardService.DeleteAsync(board.BoardId, NonToken);
            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);

            Assert.Null(boardEntity);
        }

        [Fact]
        public async Task GetAll_Success()
        {
            var user = await userService.CreateUserAsync("abc", "123", NonToken);

            var task1 = await boardService.CreateAsync(user.UserId, "abc", NonToken);
            var task2 = await boardService.CreateAsync(user.UserId, "abc1", NonToken);

            await Context.SaveChangesAsync(NonToken);
            var taskEntities = await boardService.GetAllTasksAsync(user.UserId, NonToken);
            Assert.Collection(taskEntities, it =>
            {
                Assert.Equal(user.UserId, it.BoardId);
                Assert.Equal("abc", it.Name);
            }, it =>
            {
                Assert.Equal(user.UserId, it.BoardId);
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


            await boardService.AddStatusAsync(user.UserId, "new", NonToken);

            #endregion

            await boardService.RemoveStatusAsync(user.UserId, "new", NonToken);

            await Context.SaveChangesAsync(CancellationToken.None);
            var boardEntity = await boardService.FindAsync(board.BoardId, NonToken);
            Assert.Empty(boardEntity.Statuses);
        }
    }
}