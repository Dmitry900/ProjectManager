using System.Linq.Expressions;

namespace ProjectManager.Domain.UnitOfWork
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task DeleteAsync(object id, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken = default);
    }
}