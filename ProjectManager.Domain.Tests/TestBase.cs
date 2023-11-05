using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Context;

namespace ProjectManager.Domain.Tests
{
    public class TestBase : IAsyncLifetime
    {
        readonly ServiceProvider serviceProvider;

        public IServiceProvider Services => serviceProvider;

        public TestBase()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationContext>();
            // ToDo : Make datbase fixture and inject it into this class

            serviceProvider = services.BuildServiceProvider();
        }

        #region IAsyncLifetime members

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DisposeAsync()
        {
            await serviceProvider.DisposeAsync();
        }

        #endregion
    }
}