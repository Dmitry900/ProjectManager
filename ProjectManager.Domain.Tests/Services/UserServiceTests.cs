using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Tests.Services
{
    public class UserServiceTests : TestBase
    {
        readonly IUserService userService;
        public UserServiceTests()
        {
            userService = Services.GetRequiredService<IUserService>();
        }

        #region CreateUserAsync

        [Fact]
        public async Task CreateUser_Success()
        {
            var testUser = new UserModel("Name", "123");
            await userService.CreateUserAsync(testUser, NonToken);

            await UnitOfWork.SaveAsync(CancellationToken.None);
            var users = await UnitOfWork.UserRepository.GetAsync();
            Assert.Single(users);
            var user = users.Single();
            Assert.Equal("Name", user.Name);
            Assert.Equal("123", user.Pass);
        }

        #endregion

        #region GetUserAsync
        [Fact]
        public async Task GetUser_Success()
        {
            var testUser = new UserModel("Name", "123");
            var createdUser = await userService.CreateUserAsync(testUser, NonToken);

            var user = await userService.GetUserAsync(createdUser.UserId, CancellationToken.None);
            Assert.Equal("Name", user.Name);
            Assert.Equal("123", user.PassHash);
        }
        #endregion
    }
}
