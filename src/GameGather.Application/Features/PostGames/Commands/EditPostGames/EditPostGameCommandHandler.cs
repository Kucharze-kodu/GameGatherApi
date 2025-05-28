using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.PostGames.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;


namespace GameGather.Application.Features.PostGames.Commands.EditPostGames
{
    public class EditPostGameCommandHandler : ICommandHandler<EditPostGameCommand, PostGameResponse>
    {
        private readonly IPostGameRepository _postGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public EditPostGameCommandHandler(IPostGameRepository postGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _postGameRepository=postGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PostGameResponse>> Handle(EditPostGameCommand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.PostGame.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.GameSessionId));
            PostGameId postGameId = PostGameId.Create(Convert.ToInt32(request.PostGameId));

            await _postGameRepository.EditPostGame(postGameId,sessionGameId, userId, request.GameTime, request.PostDescription);
            await _unitOfWork.SaveChangesAsync();

            return new PostGameResponse("Edited post game");
        }
    }
}
