using FluentValidation;


namespace GameGather.Application.Features.SessionGames.Commands.EditSessionGames
{
    public class EditSessionGameCommandValidator : AbstractValidator<EditSessionGameCommand>
    {
        public EditSessionGameCommandValidator() 
        {

            RuleFor(r => r)
             .Must(r => !string.IsNullOrWhiteSpace(r.Name) || !string.IsNullOrWhiteSpace(r.Description))
             .WithMessage("new name or description is required");
        }
    }
}
