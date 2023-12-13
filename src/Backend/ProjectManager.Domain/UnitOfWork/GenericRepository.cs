using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Context;

namespace ProjectManager.Domain.UnitOfWork
{
    public class GenericRepository<TEntity>(ApplicationContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        internal ApplicationContext context = context ?? throw new ArgumentNullException(nameof(context));
        internal DbSet<TEntity> dbSet = context.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken: cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken: cancellationToken);
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await dbSet.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            TEntity entityToDelete = dbSet.Find(id);
            await DeleteAsync(entityToDelete, cancellationToken);
        }

        public virtual Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken = default)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;

            return Task.CompletedTask;
        }
    }
}
