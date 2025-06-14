using FluentValidation;

namespace GameGather.Application.Features.Comments.Commands.DeleteComments
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(r => r.GameSessionId)
               .NotEmpty()
               .WithMessage("SessionId is required");
            RuleFor(r => r.CommentId)
               .NotEmpty()
               .WithMessage("Text is required");
        }
    }
}
