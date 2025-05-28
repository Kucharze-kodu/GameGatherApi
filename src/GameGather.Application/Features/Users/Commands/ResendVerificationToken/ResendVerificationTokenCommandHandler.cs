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
                TokenStatus.TokenNotReadyToResend,
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
            await _emailService.SendEmailWithVerificationTokenAsync(
                user.Email,
                user.FirstName,
                user.VerificationToken.Value.ToString(),
                request.VerifyEmailUrl,
                cancellationToken);
            user.VerificationToken.UpdateLastSendOnUtc();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new ResendVerificationTokenResponse(
            TokenStatus.TokenAlreadySent,
            user.VerificationToken.GetTimeToResendToken());
    }
}