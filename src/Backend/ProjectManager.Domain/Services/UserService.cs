using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Services
{
    internal class UserService : IUserService
    {
        readonly DbSet<UserEntity> users;

        #region IUserService members

        public async Task CreateUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            await users.AddAsync(new UserEntity
            {
                UserId = Guid.NewGuid(),
                Name = user.Name,
                Pass = user.PassHash
            });
        }

        public async Task<UserModel> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await users.FirstOrDefaultAsync(it => it.UserId == userId, cancellationToken);

            return new(user.Name, user.Pass);
        }

        #endregion
    }
}
