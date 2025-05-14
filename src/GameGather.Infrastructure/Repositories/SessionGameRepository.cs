using GameGather.Application.Persistance;
using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GameGather.Infrastructure.Repositories
{
    public class SessionGameRepository : ISessionGameRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public SessionGameRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateSessionGame(SessionGame sessionGame, CancellationToken cancellationToken = default)
        {
            await _dbContext.SessionGames.AddAsync(sessionGame);
        }


        public async Task DeleteSessionGame(SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default)
        {

            var result = await _dbContext.SessionGames.FirstOrDefaultAsync(
                                c => c.Id == sessionGameId && c.GameMasterId == userId);
            if (result == null)
            {
                return;
            }
            _dbContext.SessionGames.Remove(result);

        }

        public async Task EditSessionGame(SessionGameId sessionGameId, string name, string description, UserId userId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SessionGames.FirstOrDefaultAsync(
                                c => c.Id == sessionGameId && c.GameMasterId == userId);
            if (result == null)
            {
                return;
            }
            if (name is not null)
            {
                result.Name = name;
            }
            if (description is not null)
            {
                result.Description = description;
            }
        }

        public async Task<IEnumerable<SessionGame>> GetAllSessionGame(CancellationToken cancellationToken = default)
        {
            var resultlist = await _dbContext.SessionGames.ToListAsync();

            return resultlist;
        }

        public async Task<SessionGame?> GetSessionGame(SessionGameId sessionGameId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SessionGames.FirstOrDefaultAsync(c => c.Id == sessionGameId);

            return result;
        }


        public async Task<bool> IsThisGameMaster(UserId userId, SessionGameId sessionGameId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.SessionGames.FirstOrDefaultAsync(c => c.GameMasterId == userId && c.Id == sessionGameId);

            if( result is not null)
            {
                return true;
            }

            return false;
        }
    }
}
