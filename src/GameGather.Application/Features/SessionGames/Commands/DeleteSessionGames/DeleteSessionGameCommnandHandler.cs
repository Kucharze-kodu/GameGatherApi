using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;



namespace GameGather.Application.Features.SessionGames.Commands.DeleteSessionGames
{
    public class DeleteSessionGameCommnandHandler : ICommandHandler<DeleteSessionGameCommnand, SessionGameResponse>
    {
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSessionGameCommnandHandler(ISessionGameRepository sessionGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _sessionGameRepository=sessionGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<SessionGameResponse>> Handle(DeleteSessionGameCommnand request, CancellationToken cancellationToken)
        {

            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.SessionGame.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));
            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.Id));


            await _sessionGameRepository.DeleteSessionGame(sessionGameId, userId);
            await _unitOfWork.SaveChangesAsync();

            return new SessionGameResponse("delete session game");
        }
    }
}
