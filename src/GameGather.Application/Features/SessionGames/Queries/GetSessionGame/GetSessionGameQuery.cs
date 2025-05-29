using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.SessionGames.Queries.DTOs;

namespace GameGather.Application.Features.SessionGames.Queries.GetSessionGame
{
    public record GetSessionGameQuery(
        int IdSession
    ) : ICommand<GetSessionGameDto>;
}
