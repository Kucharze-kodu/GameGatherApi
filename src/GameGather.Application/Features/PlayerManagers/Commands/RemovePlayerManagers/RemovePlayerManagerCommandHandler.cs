using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PlayerManagers;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;



namespace GameGather.Application.Features.PlayerManagers.Commands.RemovePlayerManagers
{
    public class RemovePlayerManagerCommandHandler : ICommandHandler<RemovePlayerManagerCommand, PlayerManagerResponse>
    {

        private readonly IPlayerManagerRepository _playerManagerRepository;
        private readonly IUserContext _userContext;
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePlayerManagerCommandHandler(IPlayerManagerRepository playerManagerRepository,
            IUserContext userContext,
            ISessionGameRepository sessionGameRepository,
            IUnitOfWork unitOfWork)
        {
            _playerManagerRepository=playerManagerRepository;
            _userContext=userContext;
            _sessionGameRepository=sessionGameRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ErrorOr<PlayerManagerResponse>> Handle(RemovePlayerManagerCommand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == true)
            {
                return Errors.SessionGameList.IsNotAuthorized;
            }
            var id = _userContext.UserId;

            UserId userId = UserId.Create(Convert.ToInt32(id));
            UserId userIdGiven = UserId.Create(Convert.ToInt32(request.IdUser));
            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.SessionId));
            if (userIdGiven != userId)
            {
                if (await _sessionGameRepository.IsThisGameMaster(userId, sessionGameId))
                {
                    userId = userIdGiven;
                }

            }

            var gameSession = SessionGameList.Create(
                userId,
                sessionGameId
                );

            await _playerManagerRepository.RemovePlayerToSession(gameSession);
            await _unitOfWork.SaveChangesAsync();

            return new PlayerManagerResponse("usunięto gracza");
        }
    }
}
