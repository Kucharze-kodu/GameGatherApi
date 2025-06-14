using FluentValidation;


namespace GameGather.Application.Features.Comments.Commands.CreateComments
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
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
