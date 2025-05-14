using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGames;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.SessionGames.Commands.CreateSessionGames
{
    public class CreateSessionGameCommnandHandler : ICommandHandler<CreateSessionGameCommnand, SessionGameResponse>
    {
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSessionGameCommnandHandler(ISessionGameRepository sessionGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _sessionGameRepository=sessionGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }


        public async Task<ErrorOr<SessionGameResponse>> Handle(CreateSessionGameCommnand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if(isVerify == false)
            {
                return Errors.SessionGame.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            var nameUser = _userContext.UserName;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            var session = SessionGame.Create(
                request.Name,
                request.Description,
                userId,
                nameUser
                );

            await _sessionGameRepository.CreateSessionGame(session);
            await _unitOfWork.SaveChangesAsync();

            return new SessionGameResponse("create session game");
        }
    }
}
