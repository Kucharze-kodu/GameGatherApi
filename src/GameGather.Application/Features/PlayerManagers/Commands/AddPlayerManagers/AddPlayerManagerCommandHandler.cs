using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PlayerManagers;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGameLists;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.PlayerManagers.Commands.AddPlayerManagers
{
    public class AddPlayerManagerCommandHandler : ICommandHandler<AddPlayerManagerCommand, PlayerManagerResponse>
    {
        private readonly IPlayerManagerRepository _playerManagerRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public AddPlayerManagerCommandHandler(IPlayerManagerRepository playerManagerRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _playerManagerRepository=playerManagerRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PlayerManagerResponse>> Handle(AddPlayerManagerCommand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.SessionGameList.IsNotAuthorized;
            }
            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));
            SessionGameId sessionGameId= SessionGameId.Create(Convert.ToInt32(request.SessionId));

            var gameSession = SessionGameList.Create(
                userId,
                sessionGameId
                );


            await _playerManagerRepository.AddPlayerToSession(gameSession);
            await _unitOfWork.SaveChangesAsync();

            return new PlayerManagerResponse("add player");
        }
    }
}
