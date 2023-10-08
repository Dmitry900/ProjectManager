using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Services
{
    internal class UserService : IUserService
    {
        #region IUserService members

        public Task CreateUserAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
