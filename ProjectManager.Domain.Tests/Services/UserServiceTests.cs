using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Domain.Abstractions.Services;

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
            var createdUser = await userService.CreateUserAsync("Name", "123", NonToken);

            await Context.SaveChangesAsync(NonToken);
            var user = await userService.GetUserAsync(createdUser.UserId, NonToken);
            Assert.NotNull(user);
            Assert.Equal("Name", user.Name);
            Assert.Equal("123", user.Pass);
        }

        #endregion

        #region GetUserAsync
        [Fact]
        public async Task GetUser_Success()
        {
            var createdUser = await userService.CreateUserAsync("Name", "123", NonToken);

            var user = await userService.GetUserAsync(createdUser.UserId, CancellationToken.None);
            Assert.Equal("Name", user.Name);
            Assert.Equal("123", user.Pass);
        }
        #endregion
    }
}
