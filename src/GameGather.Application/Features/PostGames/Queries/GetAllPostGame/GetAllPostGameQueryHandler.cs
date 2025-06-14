using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;



namespace GameGather.Application.Features.PostGames.Queries.GetAllPostGame
{
    public class GetAllPostGameQueryHandler : ICommandHandler<GetAllPostGameQuery, List<GetAllPostGameResponse>>
    {
        private readonly IPostGameRepository _postGameRepository;
        private readonly IPlayerManagerRepository _playerManagerRepository;
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;

        public GetAllPostGameQueryHandler(IPostGameRepository postGameRepository,
            IPlayerManagerRepository playerManagerRepository,
            ISessionGameRepository sessionGameRepository,
            IUserContext userContext)
        {
            _postGameRepository=postGameRepository;
            _playerManagerRepository=playerManagerRepository;
            _sessionGameRepository = sessionGameRepository;
            _userContext=userContext;
        }

        public async Task<ErrorOr<List<GetAllPostGameResponse>>> Handle(GetAllPostGameQuery request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.SessionGame.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));
            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.SessionGameId));
            var isthisYourSession = await _playerManagerRepository.IsThisYourSession(userId, sessionGameId);
            var isGameMaster = await _sessionGameRepository.IsThisGameMaster(userId, sessionGameId);
            if(!isthisYourSession && ! isGameMaster)
            {
                return Errors.PostGame.IsWrongData;
            }
            var result = await _postGameRepository.GetAllPostGameSession(sessionGameId);

            if (result is null)
            {
                return Errors.PostGame.IsWrongData;
            }



            List<GetAllPostGameResponse> listOfPostGame = result.Select(x =>
                 new GetAllPostGameResponse(
                 Convert.ToInt32(x.Id.Value),
                 x.PostDescription,
                 x.DayPost,
                 x.GameTime
                 )
            ).ToList();



            return listOfPostGame;
        }
    }
}
