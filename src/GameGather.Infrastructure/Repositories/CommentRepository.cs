using GameGather.Application.Persistance;
using GameGather.Domain.Aggregates.Comments;
using GameGather.Domain.Aggregates.Comments.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace GameGather.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly GameGatherDbContext _dbContext;

        public CommentRepository(GameGatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateComment(Comment comment, CancellationToken cancellationToken = default)
        {
            await _dbContext.Comments.AddAsync(comment);
        }

        public async Task DeleteComment(CommentId commentId, SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Comments.FirstOrDefaultAsync(
                                            c => c.Id == commentId && c.SessionGameId == sessionGameId && c.UserId == userId);
            if (result == null)
            {
                return;
            }

            _dbContext.Comments.Remove(result);
        }

        public async Task<Comment> EditComment(CommentId commentId, SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Comments.FirstOrDefaultAsync(
                                            c => c.Id == commentId && c.SessionGameId == sessionGameId && c.UserId == userId);
            return result;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentSession(SessionGameId sessionGameId, CancellationToken cancellationToken = default)
        {
            var resultlist = await _dbContext.Comments.Where(c => c.SessionGameId == sessionGameId).ToListAsync();


            return resultlist;
        }
    }
}
