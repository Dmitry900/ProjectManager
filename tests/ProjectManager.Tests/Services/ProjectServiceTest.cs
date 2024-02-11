using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Services;

namespace ProjectManager.Services
{
    public class ProjectServiceTest(ApplicationFixture fixture) : IClassFixture<ApplicationFixture>
    {
        readonly IProjectService employeeService = fixture.Services.GetRequiredService<IProjectService>();
    }
}