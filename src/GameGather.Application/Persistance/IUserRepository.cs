using GameGather.Domain.Aggregates.Users;

namespace GameGather.Application.Persistance;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<int> AddUserAsync(User user, CancellationToken cancellationToken = default);
}