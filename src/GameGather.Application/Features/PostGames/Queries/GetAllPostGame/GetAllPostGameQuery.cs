using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;


namespace GameGather.Application.Features.PostGames.Queries.GetAllPostGame
{
    public record GetAllPostGameQuery(
        int SessionGameId
        ) : ICommand<List<GetAllPostGameResponse>>;
}
