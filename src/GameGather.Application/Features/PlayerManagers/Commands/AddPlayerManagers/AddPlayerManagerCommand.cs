
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PlayerManagers;

namespace GameGather.Application.Features.PlayerManagers.Commands.AddPlayerManagers
{
    public record AddPlayerManagerCommand(
        int SessionId
        ) : ICommand<PlayerManagerResponse>;
}
