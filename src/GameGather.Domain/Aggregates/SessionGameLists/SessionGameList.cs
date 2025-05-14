using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.ValueObjects;


namespace GameGather.Domain.Aggregates.SessionGameLists;

public sealed class SessionGameList
{
    public UserId UserId { get; private set; }
    public SessionGameId SessionGameId { get; private set; }
    public User User { get; private set; } = null;

    public SessionGameList() { }


    private SessionGameList(
        UserId userId,
        SessionGameId sessionGameId)
    {
        UserId = userId;
        SessionGameId = sessionGameId;
    }

    public static SessionGameList Create(UserId userId, SessionGameId sessionGameId)
    {
        return new SessionGameList(userId, sessionGameId);
    }
}
