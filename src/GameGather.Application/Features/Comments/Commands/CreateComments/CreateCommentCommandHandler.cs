using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.Comments;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.Comments.Commands.CreateComments
{
    public class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, CommentResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentCommandHandler(ICommentRepository commentRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _commentRepository=commentRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<CommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.Comment.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.GameSessionId));

            var comment = Comment.Create(
                userId,
                sessionGameId,
                request.Text
                );

            await _commentRepository.CreateComment(comment);
            await _unitOfWork.SaveChangesAsync();


            return new CommentResponse("Comment created");
        }
    }
}
