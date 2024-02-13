using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Core.Context.Entities;
using ProjectManager.Core.Models;
using ProjectManager.Core.Repositories;
using ProjectManager.Core.Services;

namespace ProjectManager.Core.Internals.Services
{
    internal class ProjectService(IRepository<Project> projectRepository, IRepository<Employee> employeeRepository, IMapper mapper) : IProjectService
    {
        readonly IRepository<Project> projectRepository = projectRepository;
        readonly IRepository<Employee> employeeRepository = employeeRepository;
        readonly IMapper mapper = mapper;

        public async Task CreateAsync(ProjectModel project, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            await projectRepository.CreateAsync(new Project
            {
                Id = project.Id,
                Title = project.Title,
                CustomerCompany = project.CustomerCompany,
                PerformingCompany = project.PerformingCompany,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                Employees = []
            }, cancellationToken);

            await employeeRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProjectModel> FindByIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var project = await projectRepository.FindByIdAsync(projectId, cancellationToken);

            return mapper.Map<ProjectModel>(project);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectsAsync(int skip = 0, int take = 10, ProjectOrderProperty orderProperty = ProjectOrderProperty.None, Order order = Order.Ascending, CancellationToken cancellationToken = default)
        {
            var projects = projectRepository.All;
            if (orderProperty == ProjectOrderProperty.None)
                if (order == Order.Ascending)
                    projects = OrderBy(projects, orderProperty);
                else if (order == Order.Descending)
                    projects = OrderByDescending(projects, orderProperty);

            return await projects.Skip(skip).Take(take).Select(it => mapper.Map<ProjectModel>(it)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<EmployeeModel>> GetProjectsEmployeesAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var project = await projectRepository.FindByIdAsync(projectId, cancellationToken);

            return project.Employees.Select(mapper.Map<EmployeeModel>);
        }

        public async Task<IEnumerable<ProjectModel>> SearchByDatesAsync(DateTime startDate, SearchDate searchDate = SearchDate.Start, CancellationToken cancellationToken = default)
        {
            var projects = projectRepository.All;

            if (searchDate == SearchDate.Start)
                projects = projects.Where(it => it.StartDate > startDate);
            else if (searchDate == SearchDate.End)
                projects = projects.Where(it => it.EndDate > startDate);

            return await projects.Select(it => mapper.Map<ProjectModel>(it)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProjectModel>> SearchByPriorityAsync(int priority, SearchPriorityOption option, CancellationToken cancellationToken = default)
        {
            var projects = projectRepository.All;
            projects = option switch
            {
                SearchPriorityOption.Above => projects.Where(it => it.Priority > priority),
                SearchPriorityOption.AboveOrEqual => projects.Where(it => it.Priority >= priority),
                SearchPriorityOption.Less => projects.Where((it) => it.Priority < priority),
                SearchPriorityOption.LessOrEqual => projects.Where((it) => it.Priority <= priority),
                SearchPriorityOption.StrictEqual => projects.Where(it => it.Priority == priority),
                _ => throw new InvalidOperationException()
            };


            return await projects.Select(it => mapper.Map<ProjectModel>(it)).ToListAsync(cancellationToken);
        }

        public async Task<ProjectModel> SearchByTitleAsync(string title, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

            var project = await projectRepository.All.FirstOrDefaultAsync(it => it.Title == title, cancellationToken);
            return mapper.Map<ProjectModel>(project);
        }

        public async Task AddEmployeeAsync(Guid projectId, Guid employeeId, CancellationToken cancellationToken = default)
        {
            var employee = await employeeRepository.FindByIdAsync(projectId, cancellationToken);
            var project = await projectRepository.FindByIdAsync(projectId, cancellationToken);

            project.Employees.Add(employee);

            await employeeRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task SetDirectorAsync(Guid projectId, Guid directorId, CancellationToken cancellationToken = default)
        {
            var employee = await employeeRepository.FindByIdAsync(projectId, cancellationToken);
            var project = await projectRepository.FindByIdAsync(projectId, cancellationToken);

            project.Director = employee;

            await employeeRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateProjectAsync(ProjectModel project, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            var entity = await projectRepository.FindByIdAsync(project.Id, cancellationToken);
            entity.Title = project.Title;
            entity.CustomerCompany = project.CustomerCompany;
            entity.PerformingCompany = project.PerformingCompany;
            entity.StartDate = project.StartDate;
            entity.EndDate = project.EndDate;
            entity.Priority = project.Priority;

            await projectRepository.UpdateAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            await employeeRepository.DeleteAsync(projectId, cancellationToken);
        }

        #region Helpers 

        static IQueryable<Project> OrderBy(IQueryable<Project> collection, ProjectOrderProperty orderProperty)
        {
            return orderProperty switch
            {
                ProjectOrderProperty.Title => collection.OrderBy(it => it.Title),
                ProjectOrderProperty.CustomerCompany => collection.OrderBy(it => it.CustomerCompany),
                ProjectOrderProperty.PerformingCompany => collection.OrderBy(it => it.PerformingCompany),
                ProjectOrderProperty.StartDate => collection.OrderBy(it => it.StartDate),
                ProjectOrderProperty.EndDate => collection.OrderBy(it => it.EndDate),
                ProjectOrderProperty.Priority => collection.OrderBy(it => it.Priority),
                _ => throw new ArgumentException("Unknown property.", nameof(orderProperty))
            };
        }

        static IQueryable<Project> OrderByDescending(IQueryable<Project> collection, ProjectOrderProperty orderProperty)
        {
            return orderProperty switch
            {
                ProjectOrderProperty.Title => collection.OrderByDescending(it => it.Title),
                ProjectOrderProperty.CustomerCompany => collection.OrderByDescending(it => it.CustomerCompany),
                ProjectOrderProperty.PerformingCompany => collection.OrderByDescending(it => it.PerformingCompany),
                ProjectOrderProperty.StartDate => collection.OrderByDescending(it => it.StartDate),
                ProjectOrderProperty.EndDate => collection.OrderByDescending(it => it.EndDate),
                ProjectOrderProperty.Priority => collection.OrderByDescending(it => it.Priority),
                _ => throw new ArgumentException("Unknown property.", nameof(orderProperty))
            };
        }

        #endregion
    }
}