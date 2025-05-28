using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;

namespace GameGather.Application.Features.SessionGames.Commands.DeleteSessionGames
{
    public record DeleteSessionGameCommnand(
        int Id
        ) : ICommand<SessionGameResponse>;
}
