
using FluentValidation;
using GameGather.Application.Features.SessionGames.Commands.CreateSessionGames;

namespace GameGather.Application.Features.PostGames.Commands.CreatePostGames
{
    public class CreatePostGameCommandValidator : AbstractValidator<CreatePostGameCommand>
    {
        public CreatePostGameCommandValidator()
        {
            RuleFor(r => r.GameSessionId)
           .NotEmpty()
           .WithMessage("SessionId is required");

            RuleFor(r => r.PostDescription)
            .NotEmpty()
            .WithMessage("Description post is required");

            RuleFor(r => r.GameTime)
            .NotEmpty()
            .WithMessage("Time of the game is required");
        }
    }
}
