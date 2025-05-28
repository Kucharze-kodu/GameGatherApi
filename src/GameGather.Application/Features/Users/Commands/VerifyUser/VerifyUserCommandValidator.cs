using FluentValidation;

namespace GameGather.Application.Features.Users.Commands.VerifyUser;

public class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
{
    public VerifyUserCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("User email is required")
            .EmailAddress()
            .WithMessage("User email is not valid");

        RuleFor(r => r.VerificationCode)
            .NotEmpty()
            .WithMessage("Verification code is required")
            .Must(x => x.ToString().Length == 36)
            .WithMessage("Verification code must be 36 characters long");
    }
}