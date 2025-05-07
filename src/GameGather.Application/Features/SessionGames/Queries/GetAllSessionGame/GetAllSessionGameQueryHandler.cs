/*using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.SessionGames.Queries.DTOs;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using MediatR;

namespace GameGather.Application.Features.SessionGames.Queries.GetAllSessionGame
{
    public class GetAllSessionGameQueryHandler : ICommandHandler<GetAllSessionGameQuery, List<GetAllSessionGameDto>>
    {
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;


        public GetAllSessionGameQueryHandler(ISessionGameRepository sessionGameRepository,
            IUserContext userContext)
        {
            _sessionGameRepository=sessionGameRepository;
            _userContext=userContext;
        }

        public async Task<ErrorOr<List<GetAllSessionGameDto>>> Handle(GetAllSessionGameQuery request, CancellationToken cancellationToken)
        {

            var result = await _sessionGameRepository.GetAllSessionGame();

            List<GetAllSessionGameDto> listOfSessionGame = result.Select(x => new GetAllSessionGameDto
            {
                Id = Convert.ToInt32(x.Id),
                Name = x.Name,
                GameMasterName = x.GameMasterName,
            }).ToList();

            return listOfSessionGame;
        }
    }
}
*/