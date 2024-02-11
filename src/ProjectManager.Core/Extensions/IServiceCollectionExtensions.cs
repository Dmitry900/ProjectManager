using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Context;
using ProjectManager.Core.Internals.Repositories;
using ProjectManager.Core.Internals.Services;
using ProjectManager.Core.Repositories;
using ProjectManager.Core.Services;

namespace ProjectManager.Core
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<ApplicationContext>(options);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}