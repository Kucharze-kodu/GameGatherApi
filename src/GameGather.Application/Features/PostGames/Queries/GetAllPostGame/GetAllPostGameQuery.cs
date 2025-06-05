using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.PostGames.Queries.DTOs;


namespace GameGather.Application.Features.PostGames.Queries.GetAllPostGame
{
    public record GetAllPostGameQuery(
        int SessionGameId
        ) : ICommand<List<GetAllPostGameDto>>;
}
