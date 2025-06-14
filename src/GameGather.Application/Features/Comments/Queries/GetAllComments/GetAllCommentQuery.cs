using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;


namespace GameGather.Application.Features.Comments.Queries.GetAllComments
{
    public record GetAllCommentQuery(
        int GameSessionId

        ) : ICommand<List<GetAllCommentResponse>>;
}
