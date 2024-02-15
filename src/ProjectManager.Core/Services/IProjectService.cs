using ProjectManager.Core.Models;

namespace ProjectManager.Core.Services
{
    public interface IProjectService
    {
        // Creating methods
        Task CreateAsync(ProjectModel project, CancellationToken cancellationToken = default);

        // Query methods
        Task<ProjectModel> FindByIdAsync(Guid projectId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProjectModel>> SearchByDatesAsync(DateTime startDate, SearchDate searchDate = SearchDate.Start, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProjectModel>> SearchByPriorityAsync(int priority, SearchPriorityOption option, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProjectModel>> GetProjectsAsync(int skip = 0, int take = 10, ProjectOrderProperty orderProperty = ProjectOrderProperty.None, Order order = Order.Ascending, CancellationToken cancellationToken = default);
        Task<ProjectModel> SearchByTitleAsync(string title, CancellationToken cancellationToken = default);

        Task<IEnumerable<EmployeeModel>> GetProjectsEmployeesAsync(Guid projectId, CancellationToken cancellationToken = default);

        // Update methods
        Task AddEmployeeAsync(Guid projectId, Guid employeeId, CancellationToken cancellationToken = default);
        Task RemoveEmployeeAsync(Guid projectId, Guid employeeId, CancellationToken cancellationToken = default);
        Task SetDirectorAsync(Guid projectId, Guid directorId, CancellationToken cancellationToken = default);
        Task UpdateProjectAsync(ProjectModel project, CancellationToken cancellationToken = default);

        // Deleting methods
        Task DeleteAsync(Guid projectId, CancellationToken cancellationToken = default);
    }
}