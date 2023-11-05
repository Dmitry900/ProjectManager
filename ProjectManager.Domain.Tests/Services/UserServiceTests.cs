using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Tests.Services
{
    public class UserServiceTests : TestBase
    {
        readonly IUserService userService;

        CancellationToken NonToken => CancellationToken.None;

        public UserServiceTests()
        {
            userService = Services.GetRequiredService<IUserService>();
        }

        #region CreateUserAsync

        [Fact]
        public async Task CreateUser_Success()
        {
            var user = new UserModel("Name", "123");
            await userService.CreateUserAsync(user, NonToken);
        }

        #endregion
    }
}
