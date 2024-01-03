using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Services
{
    internal class BoardService(IBoardContext boardContext) : IBoardService
    {
        readonly IBoardContext boardContext = boardContext;

        #region IBoardService members

        public async Task<BoardEntity> CreateAsync(Guid UserId, string Name, CancellationToken cancellationToken)
        {
            var entity = new BoardEntity
            {
                BoardId = Guid.NewGuid(),
                UserId = UserId,
                Name = Name,
                Statuses = new()
            };
            await boardContext.Boards.AddAsync(entity, cancellationToken);

            return entity;
        }
        public Task<BoardEntity> UpdateAsync(BoardModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            boardContext.Boards.Remove(board);
        }

        public Task<BoardEntity> FindAsync(Guid boardId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task AddStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            board.Statuses.Add(status);
            boardContext.Boards.Update(board);
        }

        public async Task RemoveStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            board.Statuses = board.Statuses.Where(it => it != status).ToList();
            boardContext.Boards.Update(board);
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            return board.Tasks;
        }

        #endregion
    }
}