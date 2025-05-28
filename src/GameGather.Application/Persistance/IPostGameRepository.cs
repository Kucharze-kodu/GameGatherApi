using GameGather.Domain.Aggregates.PostGames;
using GameGather.Domain.Aggregates.PostGames.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;


namespace GameGather.Application.Persistance
{
    public interface IPostGameRepository
    {

        Task CreatePostGame(PostGame postGame, CancellationToken cancellationToken = default);
        Task DeletePostGame(PostGameId postGameId,SessionGameId sessionGameId, UserId userId, CancellationToken cancellationToken = default);
        Task EditPostGame(PostGameId postGameId, SessionGameId sessionGameId, UserId userId, DateTime gameTime, string postDescription, CancellationToken cancellationToken = default);

        Task<IEnumerable<PostGame>> GetAllPostGameSession(SessionGameId sessionGameId,CancellationToken cancellationToken = default);

    }
}
