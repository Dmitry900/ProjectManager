using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;

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
        public async Task<BoardEntity> UpdateNameAsync(Guid boardId, string name, CancellationToken cancellationToken)
        {
            var entity = await FindAsync(boardId, cancellationToken);
            entity.Name = name;
            return entity;
        }

        public async Task DeleteAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            boardContext.Boards.Remove(board);
        }

        async public Task<BoardEntity> FindAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);

            return board;
        }

        public async Task AddStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            board.Statuses.Add(status);
        }

        public async Task RemoveStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            board.Statuses = board.Statuses.Where(it => it != status).ToList();
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await boardContext.Boards.FindAsync(boardId, cancellationToken);
            return board?.Tasks;
        }

        #endregion
    }
}