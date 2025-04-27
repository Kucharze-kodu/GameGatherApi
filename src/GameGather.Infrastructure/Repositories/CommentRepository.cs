using GameGather.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGather.Infrastructure.Repositories
{
    public class CommentRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public CommentRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }




    }
}
