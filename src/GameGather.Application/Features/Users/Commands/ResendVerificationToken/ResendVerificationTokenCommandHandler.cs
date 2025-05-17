using ErrorOr;
using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Users;
using GameGather.Application.Persistance;
using GameGather.Application.Utils.Email;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;

namespace GameGather.Application.Features.Users.Commands.ResendVerificationToken;

public class ResendVerificationTokenCommandHandler : ICommandHandler<ResendVerificationTokenCommand, ResendVerificationTokenResponse>
{
    private IUserRepository _userRepository;
    private IUnitOfWork _unitOfWork;
    private IEmailService _emailService;

    public ResendVerificationTokenCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, IEmailService emailService
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task<ErrorOr<ResendVerificationTokenResponse>> Handle(ResendVerificationTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        // User not found
        if (user is null)
        {
            return Errors.User.NotFound;
        }
        
        // Token already sent
        if (user.VerificationToken.CheckStatus() is TokenStatus.TokenAlreadySent)
        {
            return new ResendVerificationTokenResponse(
                "Token already sent",
                user.VerificationToken.GetTimeToResendToken());
        }
        
        // Token expired
        if (user.VerificationToken.CheckStatus() is TokenStatus.TokenExpired)
        {
            user.GenerateNewVerificationToken(request.VerifyEmailUrl);
        }
        
        // Token used
        if (user.VerificationToken.CheckStatus() is TokenStatus.TokenUsed)
        {
            return Errors.Token.Invalid;
        }
        
        // Token ready to resend
        if (user.VerificationToken.CheckStatus() is TokenStatus.TokenReadyToResend)
        {
            await ResendVerificationToken(user, request.VerifyEmailUrl);
            user.VerificationToken.UpdateLastSendOnUtc();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new ResendVerificationTokenResponse(
            "Token sent",
            user.VerificationToken.GetTimeToResendToken());
        
        
    }
    
    private async Task ResendVerificationToken(User user, string verifyEmailUrl)
    {
        var message = new EmailMessage(
            "Verify your email",
            "Welcome to GameGather",
            $$"""
              <h1>Welcome to GameGather</h1>
              <p>Hi {{user.FirstName}},</p>
              <p>Thank you for registering on GameGather.
              Please verify your email address by pass this code:
              {{user.VerificationToken.Value.ToString()}}</p>
              Or click the button below to verify your email address:</p>
              <a href="{{verifyEmailUrl}}?email={{user.Email}}&verificationCode={{user.VerificationToken.Value.ToString()}}"
                 style="display: inline-block; background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;">
                  Verify Email
              </a>
              """,
            user.Email);
        
        await _emailService.SendEmailAsync(message);
    }
}