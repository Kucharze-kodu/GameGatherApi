using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.SessionGames;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using ErrorOr;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;


namespace GameGather.Application.Features.SessionGames.Commands.EditSessionGames
{
    public class EditSessionGameCommnandHandler : ICommandHandler<EditSessionGameCommnand, SessionGameResponse>
    {
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public EditSessionGameCommnandHandler(ISessionGameRepository sessionGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _sessionGameRepository=sessionGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<SessionGameResponse>> Handle(EditSessionGameCommnand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.SessionGame.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.GameSessionId));

            await _sessionGameRepository.EditSessionGame(sessionGameId, request.Name, request.Description, userId);
            await _unitOfWork.SaveChangesAsync();

            return new SessionGameResponse("editet session game");

        }
    }
    }
