using FluentValidation;

namespace GameGather.Application.Features.Users.Commands.ResendVerificationToken;

public class ResendVerificationTokenCommandValidator : AbstractValidator<ResendVerificationTokenCommand>
{
    public ResendVerificationTokenCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");

        RuleFor(r => r.VerifyEmailUrl)
            .NotEmpty()
            .WithMessage("VerifyEmailUrl is required.");
    }
}