using GameGather.Application.Persistance;
using GameGather.Domain.Aggregates.PostGames;
using GameGather.Domain.Aggregates.PostGames.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;



namespace GameGather.Infrastructure.Repositories
{
    public class PostGameRepository : IPostGameRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public PostGameRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePostGame(PostGame postGame, CancellationToken cancellationToken = default)
        {
            await _dbContext.PostGames.AddAsync(postGame);
        }

        public async Task DeletePostGame(PostGameId postGameId, SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.PostGames.FirstOrDefaultAsync(
                                c => c.Id == postGameId && c.SessionGameId == sessionGameId && c.GameMasterId == userId);
            if (result == null)
            {
                return;
            }

            _dbContext.PostGames.Remove(result);
        }

        public async Task EditPostGame(PostGameId postGameId, SessionGameId sessionGameId, UserId userId, DateTime gameTime, string postDescription, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.PostGames.FirstOrDefaultAsync(
                               c => c.Id == postGameId && c.SessionGameId == sessionGameId && c.GameMasterId == userId);

            if (result == null)
            {
                return;
            }
            if (gameTime > DateTime.UtcNow)
            {
                result.GameTime = gameTime;
            }
            if (postDescription is not null)
            {
                result.PostDescription = postDescription;
            }
        }

        public async Task<IEnumerable<PostGame>> GetAllPostGameSession(SessionGameId sessionGameId, CancellationToken cancellationToken)
        {
            var resultlist = await _dbContext.PostGames.Where(c => c.SessionGameId == sessionGameId).ToListAsync();


            return resultlist;
        }
    }
}
