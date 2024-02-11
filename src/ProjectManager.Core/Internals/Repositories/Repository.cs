using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Context;
using ProjectManager.Core.Context.Entities.Interfaces;
using ProjectManager.Core.Repositories;

namespace ProjectManager.Core.Internals.Repositories
{
    internal class Repository<TEntity>(ApplicationContext context) : IRepository<TEntity> where TEntity : class, IEntity
    {
        readonly ApplicationContext context = context;

        public IQueryable<TEntity> All => Set.AsQueryable();

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = await Set.AddAsync(entity, cancellationToken);

            return entry.Entity;
        }

        public Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Set.FirstOrDefaultAsync(it => it.Id == id, cancellationToken);
        }

        public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = Set.Update(entity);

            return Task.FromResult(entry.State == EntityState.Modified);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await FindByIdAsync(id, cancellationToken);
            var entry = Set.Remove(entity);

            return entry.State == EntityState.Deleted;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return context.SaveChangesAsync(cancellationToken);
        }

        #region Helpers

        DbSet<TEntity> Set => context.Set<TEntity>();

        #endregion
    }
}