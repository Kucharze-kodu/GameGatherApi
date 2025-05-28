using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PlayerManagers;


namespace GameGather.Application.Features.PlayerManagers.Commands.RemovePlayerManagers
{
    public record RemovePlayerManagerCommand(
        int IdUser,
        int SessionId
        ) : ICommand<PlayerManagerResponse>;
}
