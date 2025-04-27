using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;
using GameGather.Application.Contracts.Users;

namespace GameGather.Application.Features.SessionGames.Commands.CreateSessionGames
{
    public record CreateSessionGameCommnand(
        string Name

        ) : ICommand<SessionGameResponse>;
}
