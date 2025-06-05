using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.PostGame;
using GameGather.Application.Contracts.SessionGames;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.PostGames.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.PostGames.Commands.DeletePostGames
{
    public class DeletePostGameCommandHandler : ICommandHandler<DeletePostGameCommand, PostGameResponse>
    {
        private readonly IPostGameRepository _postGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostGameCommandHandler(IPostGameRepository postGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _postGameRepository=postGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PostGameResponse>> Handle(DeletePostGameCommand request, CancellationToken cancellationToken)
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

            await _postGameRepository.DeletePostGame(postGameId,sessionGameId, userId);
            await _unitOfWork.SaveChangesAsync();

            return new PostGameResponse("Delete post game");
        }
    }
}
