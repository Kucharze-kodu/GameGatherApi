using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.Comments.ValueObcjets;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;


namespace GameGather.Application.Features.Comments.Commands.DeleteComments
{
    public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, CommentResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ISessionGameRepository _sessionGameRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository,
            ISessionGameRepository sessionGameRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _commentRepository=commentRepository;
            _sessionGameRepository=sessionGameRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<CommentResponse>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.Comment.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.GameSessionId));
            CommentId commentId = CommentId.Create(Convert.ToInt32(request.CommentId));

            var isGameMaster = await _sessionGameRepository.IsThisGameMaster(userId, sessionGameId);
            if (isGameMaster)
            {
                userId = UserId.Create(Convert.ToInt32(request.UserId));
            }

            await _commentRepository.DeleteComment(commentId,sessionGameId, userId);

            await _unitOfWork.SaveChangesAsync();


            return new CommentResponse("Comment deleted");

        }
    }
}
