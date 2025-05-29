using GameGather.Application.Persistance;
using GameGather.Infrastructure.Persistance;


namespace GameGather.Infrastructure.Repositories
{
    public class PostGameRepository : IPostGameRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public PostGameRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
