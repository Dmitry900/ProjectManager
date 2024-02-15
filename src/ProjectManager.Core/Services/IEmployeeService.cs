using ProjectManager.Core.Models;

namespace ProjectManager.Core.Services
{
    public interface IEmployeeService
    {
        Task CreateAsync(EmployeeModel employee, CancellationToken cancellationToken = default);

        Task<EmployeeModel> FindByIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EmployeeModel>> GetEmployeesAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProjectModel>> GetEmployeeProjectsAsync(Guid employeeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<EmployeeModel>> SearchByNameAsync(string name, CancellationToken cancellationToken = default);

        Task UpdateEmployeeAsync(EmployeeModel employee, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid employeeId, CancellationToken cancellationToken = default);
    }
}