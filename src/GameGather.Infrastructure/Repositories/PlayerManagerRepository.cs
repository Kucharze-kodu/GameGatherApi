

using GameGather.Application.Persistance;
using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GameGather.Infrastructure.Repositories
{
    public class PlayerManagerRepository : IPlayerManagerRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public PlayerManagerRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPlayerToSession(SessionGameList sessionGame, CancellationToken cancellationToken = default)
        {
            await _dbContext.SessionGameLists.AddAsync(sessionGame);

        }


        public async Task RemovePlayerToSession(SessionGameList sessionGame, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SessionGameLists
                .FirstOrDefaultAsync(x => x.SessionGameId == sessionGame.SessionGameId && x.UserId == sessionGame.UserId, cancellationToken);

            if (result == null)
            {
                return;
            }

            _dbContext.SessionGameLists.Remove(result);
        }
    }
}
