using ProjectManager.Domain.Context;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.UnitOfWork
{
    public class UnitOfWork(IServiceProvider provider, ApplicationContext context) : IUnitOfWork, IDisposable
    {
        readonly IServiceProvider provider = provider;
        readonly ApplicationContext context = context;
        private IGenericRepository<BoardEntity> boardRepository;
        private IGenericRepository<TaskEntity> taskRepository;
        private IGenericRepository<UserEntity> userRepository;
        private IGenericRepository<RecordEntity> recordRepository;

        public IGenericRepository<BoardEntity> BoardRepository
        {
            get
            {
                boardRepository ??= new GenericRepository<BoardEntity>(context);
                return boardRepository;
            }
        }
        public IGenericRepository<TaskEntity> TaskRepository
        {
            get
            {

                taskRepository ??= new GenericRepository<TaskEntity>(context);
                return taskRepository;
            }
        }
        public IGenericRepository<UserEntity> UserRepository
        {
            get
            {

                userRepository ??= new GenericRepository<UserEntity>(context);
                return userRepository;
            }
        }
        public IGenericRepository<RecordEntity> RecordRepository
        {
            get
            {

                recordRepository ??= new GenericRepository<RecordEntity>(context);
                return recordRepository;
            }
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        #region IDisposable members

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    public interface IUnitOfWork
    {
        IGenericRepository<BoardEntity> BoardRepository { get; }
        IGenericRepository<TaskEntity> TaskRepository { get; }
        IGenericRepository<UserEntity> UserRepository { get; }
        IGenericRepository<RecordEntity> RecordRepository { get; }
        Task SaveAsync(CancellationToken cancellationToken);
    }
}