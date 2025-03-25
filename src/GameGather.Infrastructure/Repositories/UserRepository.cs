using GameGather.Application.Persistance;
using GameGather.Domain.Aggregates.Users;
using GameGather.Infrastructure.Persistance;

namespace GameGather.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GameGatherDbContext _dbContext;

    public UserRepository(GameGatherDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}