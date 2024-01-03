using ProjectManager.Domain.Entities;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserEntity> CreateUserAsync(string Name, string PassHash, CancellationToken cancellationToken);
        Task<UserEntity> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}