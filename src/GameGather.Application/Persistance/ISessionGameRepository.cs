using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using System.Runtime.CompilerServices;

namespace GameGather.Application.Persistance
{
    public interface ISessionGameRepository
    {
        Task CreateSessionGame(SessionGame sessionGame, CancellationToken cancellationToken = default);
        Task DeleteSessionGame(SessionGameId sessionGameId, CancellationToken cancellationToken = default);
        Task EditSessionGame(SessionGameId sessionGameId, string name, string description, CancellationToken cancellationToken = default);


        Task<SessionGame?> GetSessionGame(SessionGameId sessionGameId, CancellationToken cancellationToken = default);
        Task<IEnumerable<SessionGame>> GetAllSessionGame(CancellationToken cancellationToken = default);

        Task<bool> IsThisGameMaster(UserId userId, SessionGameId sessionGameId, CancellationToken cancellationToken = default);
    }
}
