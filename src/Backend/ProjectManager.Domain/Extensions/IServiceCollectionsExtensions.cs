using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Services;

namespace ProjectManager.Domain.Extensions
{
    public static class IServiceCollectionsExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<ApplicationContext>(options);

            services.AddScoped<IBoardContext>(sp => sp.GetRequiredService<ApplicationContext>());
            services.AddScoped<IUserContext>(sp => sp.GetRequiredService<ApplicationContext>());

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