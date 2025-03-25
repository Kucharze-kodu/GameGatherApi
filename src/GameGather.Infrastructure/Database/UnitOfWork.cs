using GameGather.Application.Persistance;
using GameGather.Infrastructure.Persistance;

namespace GameGather.Infrastructure.Database;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly GameGatherDbContext _dbContext;

    public UnitOfWork(GameGatherDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }
}