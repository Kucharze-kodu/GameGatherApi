using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;

namespace GameGather.Application.Features.PostGames.Commands.EditPostGames
{
    public record EditPostGameCommand(
    int GameSessionId,
    int PostGameId,
    string PostDescription,
    DateTime GameTime
    ) : ICommand<PostGameResponse>;
}
