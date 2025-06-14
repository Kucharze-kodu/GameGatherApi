using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;

namespace GameGather.Application.Features.PostGames.Commands.CreatePostGames
{
    public record CreatePostGameCommand(
        int GameSessionId,
        string PostDescription,
        DateTime GameTime
        ) : ICommand<PostGameResponse>;
}
