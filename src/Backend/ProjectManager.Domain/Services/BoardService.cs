using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Domain.Services
{
    internal class BoardService(IUnitOfWork unitOfWork) : IBoardService
    {
        readonly IGenericRepository<BoardEntity> boardRepository = unitOfWork.BoardRepository ?? throw new ArgumentNullException(nameof(unitOfWork));
        readonly IGenericRepository<TaskEntity> taskRepository = unitOfWork.TaskRepository ?? throw new ArgumentNullException(nameof(unitOfWork));

        #region IBoardService members

        public async Task<BoardEntity> CreateAsync(BoardModel board, CancellationToken cancellationToken)
        {
            var entity = new BoardEntity
            {
                BoardId = Guid.NewGuid(),
                UserId = board.UserId,
                Name = board.Name,
                Statuses = new()
            };
            await boardRepository.InsertAsync(entity, cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(Guid boardId, CancellationToken cancellationToken)
        {
            await boardRepository.DeleteAsync(boardId, cancellationToken);
        }

        public async Task AddStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await GetBoardAsync(boardId, cancellationToken);
            board.Statuses.Add(status);
            await boardRepository.UpdateAsync(board, cancellationToken);
        }

        public async Task RemoveStatusAsync(Guid boardId, string status, CancellationToken cancellationToken)
        {
            var board = await GetBoardAsync(boardId, cancellationToken);
            board.Statuses = board.Statuses.Where(it => it != status).ToList();
            await boardRepository.UpdateAsync(board, cancellationToken);
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var boards = await taskRepository.GetAsync(it => it.BoardId == boardId, cancellationToken: cancellationToken);
            return boards.Select(it => new TaskModel(it.BoardId, it.Name));
        }

        #endregion

        Task<BoardEntity> GetBoardAsync(Guid boardId, CancellationToken cancellationToken) => boardRepository.GetByIdAsync(boardId, cancellationToken);
    }
}