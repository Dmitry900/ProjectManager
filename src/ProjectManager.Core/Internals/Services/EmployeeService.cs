using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Context.Entities;
using ProjectManager.Core.Models;
using ProjectManager.Core.Repositories;
using ProjectManager.Core.Services;

namespace ProjectManager.Core.Internals.Services
{
    internal class EmployeeService(IRepository<Project> projectRepository, IRepository<Employee> employeeRepository, IMapper mapper) : IEmployeeService
    {
        readonly IRepository<Project> projectRepository = projectRepository;
        readonly IRepository<Employee> employeeRepository = employeeRepository;
        readonly IMapper mapper = mapper;

        #region IEmployeeService members

        public async Task CreateAsync(EmployeeModel employee, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));
            await employeeRepository.CreateAsync(new Employee
            {
                Id = employee.Id,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Projects = []
            }, cancellationToken);

            await employeeRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<EmployeeModel> FindByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            var employee = await employeeRepository.FindByIdAsync(employeeId, cancellationToken);

            return mapper.Map<EmployeeModel>(employee);
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default)
        {
            var query = employeeRepository.All.Skip(skip).Take(take);

            return await query.Select(it => mapper.Map<EmployeeModel>(it)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<EmployeeModel>> SearchByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var query = employeeRepository.All.Where(it => it.FirstName.Contains(name) || it.LastName.Contains(name) || it.Patronymic.Contains(name));

            return await query.Select(it => mapper.Map<EmployeeModel>(it)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProjectModel>> GetEmployeeProjectsAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            var employee = await employeeRepository.FindByIdAsync(employeeId, cancellationToken);

            return employee.Projects.Select(it => mapper.Map<ProjectModel>(it));
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(employee, nameof(employee));

            var entity = await employeeRepository.FindByIdAsync(employee.Id, cancellationToken);
            entity.Email = employee.Email;
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.Patronymic = employee.Patronymic;

            await employeeRepository.UpdateAsync(entity, cancellationToken);
            await employeeRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid employeeId, CancellationToken cancellationToken = default)
        {
            await employeeRepository.DeleteAsync(employeeId, cancellationToken);
            await employeeRepository.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}