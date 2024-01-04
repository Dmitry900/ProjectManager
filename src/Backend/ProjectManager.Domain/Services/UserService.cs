using ProjectManager.Domain.Abstractions.Context;
using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Services
{
    internal class UserService(IUserContext userContext) : IUserService
    {
        readonly IUserContext userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

        #region IUserService members

        public async Task<UserEntity> CreateUserAsync(string Name, string PassHash, CancellationToken cancellationToken)
        {
            var entity = new UserEntity
            {
                UserId = Guid.NewGuid(),
                Name = Name,
                Pass = PassHash
            };
            await userContext.Users.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<UserEntity> FindUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await userContext.Users.FindAsync(userId, cancellationToken);

            return user;
        }

        #endregion
    }
}
