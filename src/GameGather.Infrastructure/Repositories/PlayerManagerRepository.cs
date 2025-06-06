

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
            var result = await _dbContext.SessionGameLists
                .FirstOrDefaultAsync(x => x.SessionGameId == sessionGame.SessionGameId && x.UserId == sessionGame.UserId, cancellationToken);

            if (result is not null)
            {
                return;
            }
            await _dbContext.SessionGameLists.AddAsync(sessionGame);
        }

        public async Task<IEnumerable<string>?> GetSessionPlayers(SessionGameId sessionGameId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SessionGameLists
                .Where(x=>x.SessionGameId == sessionGameId)
                .Select(x => x.User.FirstName + "" + x.User.LastName) 
                .ToListAsync(cancellationToken);

            return result;
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

        public async Task<bool> IsThisYourSession(UserId userId, SessionGameId sessionGameId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SessionGameLists.FirstOrDefaultAsync
                            (c => c.UserId == userId && c.SessionGameId == sessionGameId);

            if (result is not null)
            {
                return true;
            }

            return false;
        }
    }
}
