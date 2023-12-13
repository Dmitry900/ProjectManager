using System.Linq.Expressions;
using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Domain.Tests._fakes
{
    internal class FakeRepository<T> : IGenericRepository<T> where T : class
    {
        readonly Dictionary<Guid, T> data = [];

        #region IGenericRepository members

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = data.Values.AsQueryable();
            if (orderBy != null)
            {
                return Task.FromResult<IEnumerable<T>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<T>>(query.ToList());
            }
        }

        public Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            if (data.TryGetValue((Guid)id, out var value))
                return Task.FromResult<T>(value);
            return Task.FromResult<T>(null);
        }

        public Task InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            data.Add(GetId(entity), entity);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entityToUpdate, CancellationToken cancellationToken = default)
        {
            data[GetId(entityToUpdate)] = entityToUpdate;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            data.Remove((Guid)id);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entityToDelete, CancellationToken cancellationToken = default)
        {
            data.Remove(GetId(entityToDelete));
            return Task.CompletedTask;
        }

        #endregion

        static Guid GetId(T entity)
        {
            var type = typeof(T);

            foreach (var prop in type.GetProperties())
            {
                if (prop.Name.Contains("Id") && prop.PropertyType == typeof(Guid))
                {
                    return (Guid)prop.GetValue(entity);
                }
            }

            return Guid.Empty;
        }
    }
}