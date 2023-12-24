using ProjectManager.Domain.Abstractions.Services;
using ProjectManager.Domain.Entities;
using ProjectManager.Domain.Models;
using ProjectManager.Domain.UnitOfWork;

namespace ProjectManager.Domain.Services
{
    internal class UserService(IUnitOfWork unitOfWork) : IUserService
    {
        readonly IGenericRepository<UserEntity> repository = unitOfWork.UserRepository ?? throw new ArgumentNullException(nameof(unitOfWork));

        #region IUserService members

        public async Task<UserEntity> CreateUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var entity = new UserEntity
            {
                UserId = Guid.NewGuid(),
                Name = user.Name,
                Pass = user.PassHash
            };
            await repository.InsertAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<UserModel> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await repository.GetByIdAsync(userId, cancellationToken);

            return new(user.Name, user.Pass);
        }

        #endregion
    }
}
