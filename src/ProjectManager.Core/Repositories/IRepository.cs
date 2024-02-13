using ProjectManager.Core.Context.Entities.Interfaces;

namespace ProjectManager.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> All { get; }

        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}