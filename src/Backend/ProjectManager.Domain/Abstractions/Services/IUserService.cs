using ProjectManager.Domain.Models;

namespace ProjectManager.Domain.Abstractions.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(UserModel user, CancellationToken cancellationToken);
        Task<UserModel> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    }
}