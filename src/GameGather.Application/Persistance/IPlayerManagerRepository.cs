using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;


namespace GameGather.Application.Persistance
{
    public interface IPlayerManagerRepository
    {
        Task AddPlayerToSession(SessionGameList sessionGame, CancellationToken cancellationToken = default);
        Task RemovePlayerToSession(SessionGameList sessionGame, CancellationToken cancellationToken = default);



    }
}
