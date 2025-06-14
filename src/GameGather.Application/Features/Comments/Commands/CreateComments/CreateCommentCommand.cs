using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Comments;

namespace GameGather.Application.Features.Comments.Commands.CreateComments
{
    public record CreateCommentCommand(
        int GameSessionId,
        string Text

        ) : ICommand<CommentResponse>;
}
