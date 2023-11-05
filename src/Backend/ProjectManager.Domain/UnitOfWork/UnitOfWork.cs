using ProjectManager.Domain.Context;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext context;
        private GenericRepository<BoardEntity> boardRepository;
        private GenericRepository<TaskEntity> taskRepository;
        private GenericRepository<UserEntity> userRepository;
        private GenericRepository<RecordEntity> recordRepository;

        public GenericRepository<BoardEntity> BoardRepository
        {
            get
            {

                if (boardRepository == null)
                {
                    boardRepository = new GenericRepository<BoardEntity>(context);
                }
                return boardRepository;
            }
        }
        public GenericRepository<TaskEntity> TaskRepository
        {
            get
            {

                if (taskRepository == null)
                {
                    taskRepository = new GenericRepository<TaskEntity>(context);
                }
                return taskRepository;
            }
        }
        public GenericRepository<UserEntity> UserRepository
        {
            get
            {

                if (userRepository == null)
                {
                    userRepository = new GenericRepository<UserEntity>(context);
                }
                return userRepository;
            }
        }
        public GenericRepository<RecordEntity> RecordRepository
        {
            get
            {

                if (recordRepository == null)
                {
                    recordRepository = new GenericRepository<RecordEntity>(context);
                }
                return recordRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        #region IDisposable members

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
