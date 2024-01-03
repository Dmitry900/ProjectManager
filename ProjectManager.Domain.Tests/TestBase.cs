using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Extensions;

namespace ProjectManager.Domain.Tests
{
    public abstract class TestBase : IAsyncLifetime
    {
        static protected CancellationToken NonToken => CancellationToken.None;

        readonly ServiceProvider serviceProvider;
        public IServiceProvider Services => serviceProvider;
        public ApplicationContext Context { get; }

        public TestBase()
        {
            var services = new ServiceCollection();

            services.AddContext(options => options.UseInMemoryDatabase("test"));
            services.AddProjectManager();

            serviceProvider = services.BuildServiceProvider();
            Context = serviceProvider.GetRequiredService<ApplicationContext>();
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