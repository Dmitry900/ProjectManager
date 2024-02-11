using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Context.Entities;
using ProjectManager.Core.Models;
using ProjectManager.Core.Services;
using ProjectManager.Tests;

namespace ProjectManager.Services
{
    public class ProjectServiceTest : TestBase
    {
        readonly IProjectService projectService;

        public ProjectServiceTest()
        {
            projectService = Services.GetRequiredService<IProjectService>();
        }

        #region Test methods

        #region CRUD methods

        [Fact]
        public async Task CreateAsync_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await projectService.CreateAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // arrange
            var project = new ProjectModel(Guid.NewGuid(), "Title", "Customer", "Performer", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 5);

            // act
            await projectService.CreateAsync(project, CancellationToken.None);

            // assert
            var entity = await Repository<Project>().FindByIdAsync(project.Id, CancellationToken.None);
            Assert.NotNull(entity);

            Assert.Equal(entity.Title, project.Title);
            Assert.Equal(entity.CustomerCompany, project.CustomerCompany);
            Assert.Equal(entity.PerformingCompany, project.PerformingCompany);
            Assert.Equal(entity.StartDate, project.StartDate);
            Assert.Equal(entity.EndDate, project.EndDate);
            Assert.Equal(entity.Priority, project.Priority);

        }

        [Fact]
        public async Task FindByIdAsync_Success()
        {
            // arrange
            var project = new ProjectModel(Guid.NewGuid(), "Title", "Customer", "Performer", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 5);
            await projectService.CreateAsync(project, CancellationToken.None);

            // act 
            var found = await projectService.FindByIdAsync(project.Id);

            // assert
            Assert.NotNull(found);

            Assert.Equal(project, found);
        }

        [Fact]
        public async Task UpdateProjectAsync_Success()
        {
            // arrange
            var project = new ProjectModel(Guid.NewGuid(), "Title", "Customer", "Performer", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 5);
            await projectService.CreateAsync(project, CancellationToken.None);

            // act
            var updatedProject = new ProjectModel(project.Id, "Title1", "Customer1", "Performer1", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 7);
            await projectService.UpdateProjectAsync(updatedProject, CancellationToken.None);

            // assert
            var entity = await Repository<Project>().FindByIdAsync(project.Id, CancellationToken.None);
            Assert.NotNull(entity);

            Assert.Equal(entity.Title, updatedProject.Title);
            Assert.Equal(entity.CustomerCompany, updatedProject.CustomerCompany);
            Assert.Equal(entity.PerformingCompany, updatedProject.PerformingCompany);
            Assert.Equal(entity.StartDate, updatedProject.StartDate);
            Assert.Equal(entity.EndDate, updatedProject.EndDate);
            Assert.Equal(entity.Priority, updatedProject.Priority);

        }

        [Fact]
        public async Task DeleteProjectAsync_Success()
        {
            // arrange
            var project = new ProjectModel(Guid.NewGuid(), "Title", "Customer", "Performer", DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, 5);
            await projectService.CreateAsync(project, CancellationToken.None);

            // act
            await projectService.DeleteAsync(project.Id, CancellationToken.None);

            // assert
            var notFoundProject = await projectService.FindByIdAsync(project.Id, CancellationToken.None);
            Assert.Null(notFoundProject);
        }

        #endregion

        #region Query methods

        [Theory]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 2)]
        [InlineData(3, 2, 1)]
        [InlineData(3, 3, 0)]
        [InlineData(2, 1, 1)]
        public async Task SearchByDatesAsync_Start_Success(int startDayOffset, int endDayOffset, int count)
        {
            // arrange
            await Repository<Project>().CreateAsync(Project(startDate: DateTime.UtcNow.AddDays(-3)), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(startDate: DateTime.UtcNow.AddDays(-2)), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(startDate: DateTime.UtcNow.AddDays(-1)), CancellationToken.None);

            // act
            var collection = await projectService.SearchByDatesAsync(DateTime.UtcNow.AddDays(-startDayOffset), DateTime.UtcNow.AddDays(-endDayOffset), SearchDate.Start);

            // assert
            Assert.Equal(count, collection.Count());
        }

        [Theory]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 2)]
        [InlineData(3, 2, 1)]
        [InlineData(3, 3, 0)]
        [InlineData(2, 1, 1)]
        public async Task SearchByDatesAsync_End_Success(int startDayOffset, int endDayOffset, int count)
        {
            // arrange
            await Repository<Project>().CreateAsync(Project(startDate: DateTime.UtcNow.AddDays(-10), endDate: DateTime.UtcNow.AddDays(-3)), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(startDate: DateTime.UtcNow.AddDays(-10), endDate: DateTime.UtcNow.AddDays(-2)), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(startDate: DateTime.UtcNow.AddDays(-10), endDate: DateTime.UtcNow.AddDays(-1)), CancellationToken.None);

            // act
            var collection = await projectService.SearchByDatesAsync(DateTime.UtcNow.AddDays(-startDayOffset), DateTime.UtcNow.AddDays(-endDayOffset), SearchDate.End);

            // assert
            Assert.Equal(count, collection.Count());
        }

        [Theory]
        [InlineData(SearchPriorityOption.StrictEqual, 1)]
        [InlineData(SearchPriorityOption.Above, 5)]
        [InlineData(SearchPriorityOption.Less, 4)]
        [InlineData(SearchPriorityOption.AboveOrEqual, 6)]
        [InlineData(SearchPriorityOption.LessOrEqual, 5)]
        public async Task SearchByPriorityAsync_Success(SearchPriorityOption options, int count)
        {
            // arrange
            await Repository<Project>().CreateAsync(Project(1), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(2), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(3), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(4), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(5), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(6), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(7), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(8), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(9), CancellationToken.None);
            await Repository<Project>().CreateAsync(Project(10), CancellationToken.None);

            // act
            var collection = await projectService.SearchByPriorityAsync(5, options, CancellationToken.None);

            // assert
            Assert.Equal(count, collection.Count());
        }


        static ProjectModel[] Projects => [
            new ProjectModel(Guid.NewGuid(), "A", "A", "A", DateTime.UtcNow.AddDays(-15), DateTime.UtcNow.AddDays(-10), 1),
            new ProjectModel(Guid.NewGuid(), "B", "B", "B", DateTime.UtcNow.AddDays(-14), DateTime.UtcNow.AddDays(-9), 2),
            new ProjectModel(Guid.NewGuid(), "C", "C", "C", DateTime.UtcNow.AddDays(-13), DateTime.UtcNow.AddDays(-8), 3),
            new ProjectModel(Guid.NewGuid(), "D", "D", "D", DateTime.UtcNow.AddDays(-12), DateTime.UtcNow.AddDays(-7), 4),
            new ProjectModel(Guid.NewGuid(), "E", "E", "E", DateTime.UtcNow.AddDays(-11), DateTime.UtcNow.AddDays(-6), 5)
        ];


        // todo придумать тест
        [Fact]
        public async Task GetProjectsAsync_Pagination_Success()
        {
            // arrange
            foreach (var project in Projects)
            {
                await projectService.CreateAsync(project);
            }

            // act
            var collection = await projectService.GetProjectsAsync(2, 5);

            // assert
            Assert.Equal(3, collection.Count());

        }

        [Theory]
        [InlineData(ProjectOrderProperty.Title, Order.Ascending)]
        [InlineData(ProjectOrderProperty.CustomerCompany, Order.Ascending)]
        [InlineData(ProjectOrderProperty.PerformingCompany, Order.Ascending)]
        [InlineData(ProjectOrderProperty.StartDate, Order.Ascending)]
        [InlineData(ProjectOrderProperty.EndDate, Order.Ascending)]
        [InlineData(ProjectOrderProperty.Priority, Order.Ascending)]
        [InlineData(ProjectOrderProperty.Title, Order.Descending)]
        [InlineData(ProjectOrderProperty.CustomerCompany, Order.Descending)]
        [InlineData(ProjectOrderProperty.PerformingCompany, Order.Descending)]
        [InlineData(ProjectOrderProperty.StartDate, Order.Descending)]
        [InlineData(ProjectOrderProperty.EndDate, Order.Descending)]
        [InlineData(ProjectOrderProperty.Priority, Order.Descending)]
        public async Task GetProjectsAsync_Sorting_Success(ProjectOrderProperty orderProperty, Order order)
        {
            // arrange
            foreach (var project in Projects)
            {
                await projectService.CreateAsync(project);
            }

            // act
            var collection = await projectService.GetProjectsAsync(orderProperty: orderProperty, order: order);

            // assert
            if (order == Order.Ascending)
                Assert.Equal(Projects, collection);
            else Assert.Equal(Projects.Reverse(), collection);
        }

        [Fact]
        public async Task SearchByTitleAsync_Success()
        {
            // arrange
            var project = Project();
            await Repository<Project>().CreateAsync(Project(), CancellationToken.None);

            // act
            var found = await projectService.SearchByTitleAsync(project.Title, CancellationToken.None);

            // assert
            Assert.Equal(found.Title, project.Title);
        }

        #endregion

        #region Employee managment

        [Fact]
        public async Task GetProjectsEmployeesAsync_Success()
        {
            // arrange
            var project = Project();
            project.Employees.AddRange([
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Email = "mail",
                    FirstName = "Test",
                    LastName = "Test",
                    Patronymic = "Test"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Email = "mail",
                    FirstName = "Test",
                    LastName = "Test",
                    Patronymic = "Test"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Email = "mail",
                    FirstName = "Test",
                    LastName = "Test",
                    Patronymic = "Test"
                },
            ]);
            await Repository<Project>().CreateAsync(project, CancellationToken.None);

            // act
            var found = await projectService.GetProjectsEmployeesAsync(project.Id, CancellationToken.None);

            // assert
            Assert.Equal(3, found.Count());
        }

        [Fact]
        public async Task AddEmployeeAsync_Success()
        {
            // arrange
            var project = Project();
            await Repository<Project>().CreateAsync(project, CancellationToken.None);
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = "mail",
                FirstName = "Test",
                LastName = "Test",
                Patronymic = "Test"
            };
            await Repository<Employee>().CreateAsync(employee, CancellationToken.None);

            // act
            await projectService.AddEmployeeAsync(project.Id, employee.Id);

            // assert
            var entity = await Repository<Project>().FindByIdAsync(project.Id, CancellationToken.None);
            Assert.Single(entity.Employees);

            var entityEmployee = entity.Employees[0];
            Assert.Equal(employee.Id, entityEmployee.Id);
        }

        [Fact]
        public async Task SetDirectorAsync_Success()
        {
            // arrange
            var project = Project();
            await Repository<Project>().CreateAsync(project, CancellationToken.None);
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Email = "mail",
                FirstName = "Test",
                LastName = "Test",
                Patronymic = "Test"
            };
            await Repository<Employee>().CreateAsync(employee, CancellationToken.None);

            // act
            await projectService.SetDirectorAsync(project.Id, employee.Id);

            // assert
            var entity = await Repository<Project>().FindByIdAsync(project.Id, CancellationToken.None);
            Assert.NotNull(entity.Director);
            Assert.Equal(employee.Id, entity.Director.Id);
        }

        #endregion

        #endregion
    }
}