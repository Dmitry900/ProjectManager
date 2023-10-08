using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Services
{
    internal class BoardService : IBoardService
    {
        readonly DbSet<BoardEntity> boards;

        public BoardService(IBoardContext boardContext)
        {
            boards = boardContext.Boards ?? throw new ArgumentNullException(nameof(boardContext));
        }

        #region IBoardService members

        public async Task CreateAsync(BoardModel board, CancellationToken cancellationToken)
        {
            var entity = new BoardEntity
            {
                BoardId = Guid.NewGuid(),
                UserId = board.UserId,
                Name = board.Name,
            };
            await boards.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await GetBoardAsync(boardId, cancellationToken);
            boards.Remove(board);
        }

        public async Task AddStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await GetBoardAsync(boardId, cancellationToken);
            board.Statuses.Add(status);
        }

        public async Task RemoveStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await GetBoardAsync(boardId, cancellationToken);
            board.Statuses.Remove(status);
        }

        public async Task<IEnumerable<BoardModel>> GetAllTasksAsync(Guid boardId, CancellationToken cancellationToken)
        {
            return await boards.Where(it => it.BoardId == boardId).Select(it => new BoardModel(it.UserId, it.Name)).ToListAsync(cancellationToken);
        }

        #endregion

        Task<BoardEntity> GetBoardAsync(Guid boardId, CancellationToken cancellationToken) => boards.FirstOrDefaultAsync(it => it.BoardId == boardId, cancellationToken);
    }
}
