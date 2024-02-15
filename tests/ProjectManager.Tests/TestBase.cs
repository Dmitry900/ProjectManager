using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core;
using ProjectManager.Core.Context;
using ProjectManager.Core.Context.Entities;
using ProjectManager.Core.Context.Entities.Interfaces;
using ProjectManager.Core.Profiles;
using ProjectManager.Core.Repositories;

namespace ProjectManager.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        readonly ServiceProvider serviceProvider;
        readonly IServiceScope serviceScope;

        readonly ApplicationContext context;

        public IServiceProvider Services => serviceScope.ServiceProvider;
        protected ApplicationContext Context => context;

        public TestBase()
        {
            var services = new ServiceCollection();

            services.AddContext(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()))
                    .AddApplicationServices();

            services.AddAutoMapper(it => it.AddProfile<MappingProfile>());

            OnConfigure(services);

            serviceProvider = services.BuildServiceProvider();
            serviceScope = serviceProvider.CreateScope();

            context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
            => Services.GetRequiredService<IRepository<TEntity>>();
        protected virtual void OnConfigure(IServiceCollection services) { }

        #region IAsyncLifetime members

        public async Task InitializeAsync()
        {
            await context.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await context.Database.EnsureDeletedAsync();
            serviceScope.Dispose();
            await serviceProvider.DisposeAsync();
        }

        #endregion

        protected static Project Project(int? priority = null, DateTime? startDate = null, DateTime? endDate = null) => new()
        {
            Id = Guid.NewGuid(),
            Title = Guid.NewGuid().ToString(),
            PerformingCompany = "Performer",
            CustomerCompany = "Customer",
            StartDate = startDate ?? DateTime.UtcNow.AddDays(-1),
            EndDate = endDate ?? DateTime.UtcNow,
            Priority = priority ?? Random.Shared.Next(10),
            Employees = []
        };
    }
}