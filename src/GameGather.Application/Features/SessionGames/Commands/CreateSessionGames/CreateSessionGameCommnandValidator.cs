using FluentValidation;

namespace GameGather.Application.Features.SessionGames.Commands.CreateSessionGames
{
    public class CreateSessionGameCommnandValidator : AbstractValidator<CreateSessionGameCommnand>
    {
        public CreateSessionGameCommnandValidator()
        {
            RuleFor(r => r.Name)
           .NotEmpty()
           .WithMessage("Name is required");

        }
    }
}