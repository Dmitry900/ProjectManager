using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Services;
using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Domain.Extensions
{
    public static class IServiceCollectionsExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<ApplicationContext>(options);

            services.AddSingleton<IUnitOfWork, Domain.UnitOfWork.UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddProjectManager(this IServiceCollection services)
        {
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}