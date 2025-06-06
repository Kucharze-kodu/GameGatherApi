using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;


namespace GameGather.Application.Features.PostGames.Commands.DeletePostGames
{
    public record DeletePostGameCommand
    (
        int GameSessionId,
        int PostGameId

    ): ICommand<PostGameResponse>;
}
