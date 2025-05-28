using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.SessionGames.Queries.DTOs;


namespace GameGather.Application.Features.SessionGames.Queries.GetAllSessionGame
{
    public record class GetAllSessionGameQuery(
        ) : ICommand<List<GetAllSessionGameDto>>;
}
