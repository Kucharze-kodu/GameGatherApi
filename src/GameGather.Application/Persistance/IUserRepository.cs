using GameGather.Domain.Aggregates.Users;

namespace GameGather.Application.Persistance;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> AnyUserAsync(string email, CancellationToken cancellationToken = default);
    Task<int?> GetIdByEmailAsync(string email, CancellationToken cancellationToken = default);
}