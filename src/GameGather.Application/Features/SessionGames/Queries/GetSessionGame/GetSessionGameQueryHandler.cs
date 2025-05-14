using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Features.SessionGames.Queries.DTOs;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.SessionGames.Queries.GetSessionGame
{
    public class GetSessionGameQueryHandler : ICommandHandler<GetSessionGameQuery, GetSessionGameDto>
    {
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;
        private readonly IPlayerManagerRepository _playerManagerRepository;

        public GetSessionGameQueryHandler(ISessionGameRepository sessionGameRepository,
            IPlayerManagerRepository playerManagerRepository,
            IUserContext userContext)
        {
            _sessionGameRepository=sessionGameRepository;
            _playerManagerRepository=playerManagerRepository;
            _userContext=userContext;
        }


        public async Task<ErrorOr<GetSessionGameDto>> Handle(GetSessionGameQuery request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.SessionGame.IsNotAuthorized;
            }

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.IdSession));
            var result = await _sessionGameRepository.GetSessionGame(sessionGameId);

            if(result is null)
            {
                return Errors.SessionGame.IsWrongData;
            }

            var listPlayerSession = await _playerManagerRepository.GetSessionPlayers(sessionGameId);

            GetSessionGameDto sessionGame = new GetSessionGameDto();

            sessionGame.Id = Convert.ToInt32(result.Id.Value);
            sessionGame.Name = result.Name;
            sessionGame.Description = result.Description;
            sessionGame.GameMasterName = result.GameMasterName;
            sessionGame.PlayerName = listPlayerSession.ToList();



            return sessionGame;
        }
    }
}
