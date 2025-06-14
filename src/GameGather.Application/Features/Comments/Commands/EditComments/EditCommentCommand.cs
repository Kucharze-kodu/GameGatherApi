using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;


namespace GameGather.Application.Features.Comments.Commands.EditComments
{
    public record EditCommentCommand(
        int CommentId,
        int GameSessionId,
        string Text

        ) : ICommand<CommentResponse>;
}
