using FluentValidation;

namespace GameGather.Application.Features.Users.Commands.VerifyUser;

public class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
{
    public VerifyUserCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(r => r.Token)
            .NotEmpty()
            .WithMessage("Token is required")
            .Must(x => x.ToString().Length == 36)
            .WithMessage("Token must be 36 characters long");
    }
}