
using Microsoft.Extensions.DependencyInjection;

namespace ProjectManager
{
    public class ApplicationFixture : IDisposable
    {
        readonly ServiceProvider serviceProvider;

        public IServiceProvider Services => serviceProvider;
        public ApplicationFixture()
        {
            var services = new ServiceCollection();

            serviceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            serviceProvider.Dispose();
        }
    }
}
