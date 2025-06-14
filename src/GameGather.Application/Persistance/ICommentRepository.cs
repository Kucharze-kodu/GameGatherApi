using GameGather.Domain.Aggregates.Comments;
using GameGather.Domain.Aggregates.Comments.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;

namespace GameGather.Application.Persistance
{
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment, CancellationToken cancellationToken = default);
        Task DeleteComment(CommentId commentId, SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default);
        Task<Comment> EditComment(CommentId commentId, SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Comment>> GetAllCommentSession(SessionGameId sessionGameId, CancellationToken cancellationToken = default);


    }
}
