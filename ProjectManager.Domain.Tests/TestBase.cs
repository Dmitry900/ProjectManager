using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Extensions;
using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Domain.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        static protected CancellationToken NonToken => CancellationToken.None;

        readonly ServiceProvider serviceProvider;
        public IServiceProvider Services => serviceProvider;
        public IUnitOfWork UnitOfWork { get; }

        public TestBase()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("test"));
            services.AddSingleton<IUnitOfWork, Domain.UnitOfWork.UnitOfWork>();
            services.AddProjectManager();

            serviceProvider = services.BuildServiceProvider();
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        #region Abstract 

        public virtual void OnConfigure(IServiceCollection services)
        {

        }

        #endregion


        #region IAsyncLifetime members

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            await serviceProvider.DisposeAsync();
        }

        #endregion
    }
}