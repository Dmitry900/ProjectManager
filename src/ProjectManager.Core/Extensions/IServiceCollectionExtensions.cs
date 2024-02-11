using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Context;
using ProjectManager.Core.Internals.Repositories;
using ProjectManager.Core.Repositories;

namespace ProjectManager.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<ApplicationContext>(options);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}