using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Core.Context.Entities;
using ProjectManager.Core.Models;
using ProjectManager.Core.Services;
using ProjectManager.Tests;

namespace EmployeeManager.Tests.Services
{
    public class EmployeeServiceTest : TestBase
    {
        readonly IEmployeeService employeeService;

        public EmployeeServiceTest()
        {
            employeeService = Services.GetRequiredService<IEmployeeService>();
        }

        #region Test methods

        #region CRUD methods

        [Fact]
        public async Task CreateAsync_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await employeeService.CreateAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            // arrange
            var employee = new EmployeeModel(Guid.NewGuid(), "Test1", "Test2", "Test3", "Mail");

            // act
            await employeeService.CreateAsync(employee, CancellationToken.None);

            // assert
            var entity = await Repository<Employee>().FindByIdAsync(employee.Id, CancellationToken.None);
            Assert.NotNull(entity);

            Assert.Equal(entity.FirstName, employee.FirstName);
            Assert.Equal(entity.LastName, employee.LastName);
            Assert.Equal(entity.Patronymic, employee.Patronymic);
            Assert.Equal(entity.Email, employee.Email);

        }

        [Fact]
        public async Task FindByIdAsync_Success()
        {
            // arrange
            var employee = new EmployeeModel(Guid.NewGuid(), "Test1", "Test2", "Test3", "Mail");
            await employeeService.CreateAsync(employee, CancellationToken.None);

            // act 
            var found = await employeeService.FindByIdAsync(employee.Id);

            // assert
            Assert.NotNull(found);

            Assert.Equal(employee, found);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_Success()
        {
            // arrange
            var employee = new EmployeeModel(Guid.NewGuid(), "Test1", "Test2", "Test3", "Mail");
            await employeeService.CreateAsync(employee, CancellationToken.None);

            // act
            var updatedEmployee = new EmployeeModel(employee.Id, "Test7", "Test6", "Test5", "Mail1");
            await employeeService.UpdateEmployeeAsync(updatedEmployee, CancellationToken.None);

            // assert
            var entity = await Repository<Employee>().FindByIdAsync(employee.Id, CancellationToken.None);
            Assert.NotNull(entity);

            Assert.Equal(entity.FirstName, updatedEmployee.FirstName);
            Assert.Equal(entity.LastName, updatedEmployee.LastName);
            Assert.Equal(entity.Patronymic, updatedEmployee.Patronymic);
            Assert.Equal(entity.Email, updatedEmployee.Email);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_Success()
        {
            // arrange
            var employee = new EmployeeModel(Guid.NewGuid(), "Test1", "Test2", "Test3", "Mail");
            await employeeService.CreateAsync(employee, CancellationToken.None);

            // act
            await employeeService.DeleteAsync(employee.Id, CancellationToken.None);

            // assert
            var notFoundEmployee = await employeeService.FindByIdAsync(employee.Id, CancellationToken.None);
            Assert.Null(notFoundEmployee);
        }

        #endregion

        #region Query methods

        static readonly EmployeeModel[] Employees = [
            new EmployeeModel(Guid.NewGuid(), "Ivanov", "Ivan", "Ivanovich", "Mail"),
            new EmployeeModel(Guid.NewGuid(), "Petrov", "Ivan", "Ivanovich", "Mail"),
            new EmployeeModel(Guid.NewGuid(), "Petrov", "Petr", "Ivanovich", "Mail"),
            new EmployeeModel(Guid.NewGuid(), "Olegov", "Oleg", "Petrovich", "Mail")
        ];

        public static TheoryData<string, EmployeeModel[]> TestObject => new()
        {
            { "Ivan", new EmployeeModel[] { Employees[0], Employees[1], Employees[2] } },
            { "Ivanovich", new EmployeeModel[] { Employees[0], Employees[1], Employees[2] } },
            { "Petr", new EmployeeModel[] {  Employees[1], Employees[2], Employees[3] } },
            { "Oleg", new EmployeeModel[] { Employees[3] } }
        };

        [Fact]
        public async Task GetEmployeesAsync_Success()
        {
            // arrange
            foreach (var employee in Employees)
            {
                await employeeService.CreateAsync(employee, CancellationToken.None);
            }

            // act
            var collection = await employeeService.GetEmployeesAsync(2, 5);

            // assert
            Assert.Equal(2, collection.Count());
        }

        [Theory]
        [MemberData(nameof(TestObject))]
        public async Task SearchByNameAsync_Success(string search, EmployeeModel[] found)
        {
            // arrange
            foreach (var employee in Employees)
            {
                await employeeService.CreateAsync(employee, CancellationToken.None);
            }

            // act
            var variants = await employeeService.SearchByNameAsync(search);

            // assert
            Assert.Equal(found, variants);
        }

        #endregion

        #region Project managemnt

        [Fact]
        public async Task GetProjectsEmployeesAsync_Success()
        {
            // arrange
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Email = "mail",
                FirstName = "Test",
                LastName = "Test",
                Patronymic = "Test"
            };
            employee.Projects.AddRange([Project(), Project(), Project()]);
            await Repository<Employee>().CreateAsync(employee, CancellationToken.None);
            await Repository<Employee>().SaveChangesAsync();

            // act
            var found = await employeeService.GetEmployeeProjectsAsync(employee.Id, CancellationToken.None);

            // assert
            Assert.Equal(3, found.Count());
        }

        #endregion

        #endregion
    }
}