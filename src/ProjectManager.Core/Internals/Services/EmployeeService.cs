using ProjectManager.Core.Models;
using ProjectManager.Core.Services;

namespace ProjectManager.Core.Internals.Services
{
    internal class EmployeeService : IEmployeeService
    {
        public Task CreateAsync(EmployeeModel employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeModel> FindByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectModel>> GetEmployeeProjectsAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeModel>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployeeAsync(EmployeeModel employee, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}