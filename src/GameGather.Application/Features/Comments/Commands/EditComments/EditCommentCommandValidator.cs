using FluentValidation;

namespace GameGather.Application.Features.Comments.Commands.EditComments
{
    internal class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(r => r.GameSessionId)
               .NotEmpty()
               .WithMessage("SessionId is required");
            RuleFor(r => r.Text)
               .NotEmpty()
               .WithMessage("Text is required");
        }
    }
}
