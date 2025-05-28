using FluentValidation;


namespace GameGather.Application.Features.PostGames.Commands.DeletePostGames
{
    public class DeletePostGameCommandValidator : AbstractValidator<DeletePostGameCommand>
    {
        public DeletePostGameCommandValidator()
        {
            RuleFor(r => r.GameSessionId)
            .NotEmpty()
            .WithMessage("GameSessionId is required");

            RuleFor(r => r.PostGameId)
            .NotEmpty()
            .WithMessage("PostGameId is required");
        }
    }
}
