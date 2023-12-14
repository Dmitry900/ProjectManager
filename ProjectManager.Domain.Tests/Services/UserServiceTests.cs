using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Tests.Services
{
    public class UserServiceTests : TestBase
    {
        readonly IUserService userService;

        static CancellationToken NonToken => CancellationToken.None;

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
    }
}
