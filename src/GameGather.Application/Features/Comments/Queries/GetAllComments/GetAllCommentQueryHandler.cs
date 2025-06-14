using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.SessionGames.ValueObcjects;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.Comments.Queries.GetAllComments
{
    public class GetAllCommentQueryHandler : ICommandHandler<GetAllCommentQuery, List<GetAllCommentResponse>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPlayerManagerRepository _playerManagerRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCommentQueryHandler(ICommentRepository commentRepository,
            IPlayerManagerRepository playerManagerRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork)
        {
            _commentRepository=commentRepository;
            _playerManagerRepository=playerManagerRepository;
            _userContext=userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<GetAllCommentResponse>>> Handle(GetAllCommentQuery request, CancellationToken cancellationToken)
        {
            var isVerify = _userContext.IsAuthenticated;
            if (isVerify == false)
            {
                return Errors.Comment.IsNotAuthorized;
            }

            var id = _userContext.UserId;
            UserId userId = UserId.Create(Convert.ToInt32(id));

            SessionGameId sessionGameId = SessionGameId.Create(Convert.ToInt32(request.GameSessionId));

            var isthisYourSession = await _playerManagerRepository.IsThisYourSession(userId, sessionGameId);

            if (!isthisYourSession)
            {
                return Errors.PostGame.IsWrongData;
            }

            var result = await _commentRepository.GetAllCommentSession(sessionGameId);

            if (result is null)
            {
                return Errors.Comment.IsWrongData;
            }

            List<GetAllCommentResponse> listOfComment = result.Select(x =>
                 new GetAllCommentResponse(
                 Convert.ToInt32(x.Id.Value),
                 x.Text,
                 x.DateComment
                 )
            ).ToList();


            return listOfComment;
        }
    }
}
