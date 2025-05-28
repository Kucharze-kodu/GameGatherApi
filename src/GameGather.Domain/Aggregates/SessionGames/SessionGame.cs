using GameGather.Domain.Aggregates.Comments;
using GameGather.Domain.Aggregates.PostGames;
using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Primitives;


namespace GameGather.Domain.Aggregates.SessionGames;

public sealed class SessionGame : AggregateRoot<SessionGameId>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public UserId GameMasterId { get; private set; }
    public string GameMasterName { get; private set; }


    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<PostGame> PostGames { get; private set; } = new List<PostGame>();


    private readonly List<SessionGameList> _sessionGameList = new();
    public IReadOnlyCollection<SessionGameList> SessionGameLists => _sessionGameList.AsReadOnly();


    private SessionGame(SessionGameId id) : base(id)
    {
    }

    private SessionGame(SessionGameId id, 
        string name,
        string description,
        UserId gameMasterId, 
        string gameMasterName): base(id)
    {
        Name = name;
        Description = description;
        GameMasterId = gameMasterId;
        GameMasterName = gameMasterName;
    }

    public static SessionGame Create(string name,string description, UserId gameMasterId, string gameMasterName)
    {
        var sessionGame = new SessionGame(
            default,
            name,
            description,
            gameMasterId,
            gameMasterName);

        return sessionGame;
    }

    public SessionGame Load(
        SessionGameId id,
        string name,
        string description,
        UserId gameMasterId,
        string gameMasterName)
    {
        Id = id;
        Name = name;
        Description= description;
        GameMasterId = gameMasterId;
        GameMasterName = gameMasterName;
        return this;
    }
}
