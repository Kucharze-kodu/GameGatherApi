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
    public DateTime DayPost {  get; set; }
    public DateTime GameTime { get; set; }
    public string PostDescription { get; set; }
    public SessionGame SessionGame { get; private set; } = null;

    private PostGame(PostGameId id) : base(id)
    {
    }


    private PostGame(PostGameId id,
        UserId gameMasterId,
        SessionGameId sessionGameId,
        DateTime gameTime,
        string postDescription) : base(id)
    {
        GameMasterId = gameMasterId;
        SessionGameId = sessionGameId;
        DayPost = DateTime.Now;
        GameTime = gameTime;
        PostDescription=postDescription;
    }

    public static PostGame Create(UserId gameMasterId, SessionGameId sessionGameId, DateTime gameTime, string postDescription)
    {
        var postGame = new PostGame(
            default,
            gameMasterId,
            sessionGameId,
            gameTime,
            postDescription);

        return postGame;
    }

    public PostGame Load(
        PostGameId id,
        UserId gameMasterId,
        DateTime dayPost,
        DateTime gameTime,
        string postDescription)
    {
        Id = id;
        GameMasterId = gameMasterId;
        DayPost = dayPost;
        GameTime = gameTime;
        PostDescription = postDescription;
        return this;
    }
}
