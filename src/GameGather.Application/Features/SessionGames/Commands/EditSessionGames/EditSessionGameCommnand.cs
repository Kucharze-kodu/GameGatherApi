using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;

namespace GameGather.Application.Features.SessionGames.Commands.EditSessionGames
{
    public record EditSessionGameCommnand(
        ) : ICommand<SessionGameResponse>;
}
