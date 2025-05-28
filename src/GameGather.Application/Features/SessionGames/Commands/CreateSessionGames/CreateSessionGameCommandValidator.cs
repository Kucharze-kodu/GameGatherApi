using FluentValidation;

namespace GameGather.Application.Features.SessionGames.Commands.CreateSessionGames
{
    public class CreateSessionGameCommandValidator : AbstractValidator<CreateSessionGameCommand>
    {
        public CreateSessionGameCommandValidator()
        {
            RuleFor(r => r.Name)
           .NotEmpty()
           .WithMessage("Name is required");

        }
    }
}