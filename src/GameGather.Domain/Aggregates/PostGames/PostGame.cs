using GameGather.Domain.Aggregates.PostGames.Enums;
using GameGather.Domain.Aggregates.PostGames.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Primitives;



namespace GameGather.Domain.Aggregates.PostGames;

public sealed class PostGame : AggregateRoot<PostGameId>
{
    public UserId GameMasterId { get; private set; }
    public SessionGameId SessionGameId { get; private set; }
    public DateTime DayPost {  get; private set; }
    public DateTime GameTime { get; private set; }
    public State State { get; private set; }


    private PostGame(PostGameId id) : base(id)
    {
    }


    private PostGame(PostGameId id,
        UserId gameMasterId,
        SessionGameId sessionGameId,
        DateTime gameTime,
        State state) : base(id)
    {
        GameMasterId = gameMasterId;
        SessionGameId = sessionGameId;
        DayPost = DateTime.Now;
        GameTime = gameTime;
        State = state;
    }

    public static PostGame Create(UserId gameMasterId, SessionGameId sessionGameId, DateTime gameTime, State state)
    {
        var postGame = new PostGame(
            default,
            gameMasterId,
            sessionGameId,
            gameTime,
            state);

        return postGame;
    }

    public PostGame Load(
        SessionGameId id,
        string name,
        UserId gameMasterId,
        string gameMasterName)
    {
        Id = id;
        Name = name;
        GameMasterId = gameMasterId;
        GameMasterName = gameMasterName;
        return this;
    }
}
