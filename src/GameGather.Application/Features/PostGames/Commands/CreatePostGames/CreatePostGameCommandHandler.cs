using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.PostGames;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;


namespace GameGather.Application.Features.PostGames.Commands.CreatePostGames
{
    public class CreatePostGameCommandHandler : ICommandHandler<CreatePostGameCommand, PostGameResponse>
    {
        private readonly IPostGameRepository _postGameRepository;
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostGameCommandHandler(IPostGameRepository postGameRepository,
            ISessionGameRepository sessionGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _postGameRepository=postGameRepository;
            _sessionGameRepository=sessionGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PostGameResponse>> Handle(CreatePostGameCommand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.PostGame.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.GameSessionId));
            var isThisGameMasterSession = await _sessionGameRepository.IsThisGameMaster(userId, sessionGameId);


            if (_userContext.Role != "Admin")
            {
                if (!isThisGameMasterSession)
                {
                    return Errors.PostGame.IsNotAuthorized;
                }
            }


            var postGame = PostGame.Create
            (
                userId,
                sessionGameId,
                request.GameTime,
                request.PostDescription
                );

            await _postGameRepository.CreatePostGame(postGame);
            await _unitOfWork.SaveChangesAsync();


            return new PostGameResponse("Post game created");
        }
    }
}
