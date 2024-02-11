using ProjectManager.Core.Models;
using ProjectManager.Core.Services;

namespace ProjectManager.Core.Internals.Services
{
    internal class ProjectService : IProjectService
    {
        public Task AddEmployeeAsync(Guid projectId, Guid employeeId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(ProjectModel project, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectModel> FindByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectModel>> GetProjectsAsync(int skip = 0, int take = 10, ProjectOrderProperty orderProperty = ProjectOrderProperty.None, Order order = Order.Ascending, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeModel>> GetProjectsEmployeesAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectModel>> SearchByDatesAsync(DateTime startDate, DateTime endDate = default, SearchDate searchDate = SearchDate.Start, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectModel>> SearchByPriorityAsync(int priority, SearchPriorityOption option, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectModel> SearchByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SetDirectorAsync(Guid projectId, Guid directorId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectAsync(ProjectModel project, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}