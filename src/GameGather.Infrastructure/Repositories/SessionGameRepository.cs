using GameGather.Application.Persistance;
using GameGather.Infrastructure.Persistance;


namespace GameGather.Infrastructure.Repositories
{
    public class SessionGameRepository : ISessionGameRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public SessionGameRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
