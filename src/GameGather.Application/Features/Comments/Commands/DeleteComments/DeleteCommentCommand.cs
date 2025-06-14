using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;


namespace GameGather.Application.Features.Comments.Commands.DeleteComments
{
    public record DeleteCommentCommand(
        int CommentId,
        int GameSessionId,
        int UserId
        ) : ICommand<CommentResponse>;
}
