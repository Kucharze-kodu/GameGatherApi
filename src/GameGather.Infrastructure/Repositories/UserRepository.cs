using GameGather.Application.Persistance;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GameGather.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GameGatherDbContext _dbContext;

    public UserRepository(GameGatherDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext
            .Users
            .FirstOrDefaultAsync(r => r.Email == email, cancellationToken);
        
        return user;
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext
            .Users
            .FirstOrDefaultAsync(r => r.Id.Equals(UserId.Create(id)), cancellationToken);
        
        return user;
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext
            .Users
            .AddAsync(user, cancellationToken);
    }

    public Task<bool> AnyUserAsync(string email, CancellationToken cancellationToken = default)
    {
        return _dbContext
            .Users
            .AnyAsync(r => r.Email == email, cancellationToken);
    }

    public async Task<int?> GetIdByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var userId = await _dbContext
            .Users
            .Where(r => r.Email == email)
            .Select(r => r.Id.Value)
            .FirstOrDefaultAsync(cancellationToken);
        
        return userId;
    }




}