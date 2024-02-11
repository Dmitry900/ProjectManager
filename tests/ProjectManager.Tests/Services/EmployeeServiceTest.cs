using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Services;

namespace ProjectManager.Tests.Services
{
    public class EmployeeServiceTest(ApplicationFixture fixture) : IClassFixture<ApplicationFixture>
    {
        readonly IEmployeeService employeeService = fixture.Services.GetRequiredService<IEmployeeService>();
    }
}