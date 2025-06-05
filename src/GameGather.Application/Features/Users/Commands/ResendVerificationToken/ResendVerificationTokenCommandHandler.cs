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
        IUnitOfWork unitOfWork, 
        IEmailService emailService
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
        
        var tokenStatus = user.VerificationToken.CheckStatus();
        
        // Token used
        if (tokenStatus is TokenStatus.Used)
        {
            return Errors.Token.Invalid;
        }
        
        // Token expired
        if (tokenStatus is TokenStatus.Expired)
        {
            user.GenerateNewVerificationToken(request.VerifyEmailUrl);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        // Token is not sent yet
        if (tokenStatus is TokenStatus.NotSent)
        {
            await _emailService.SendEmailWithVerificationTokenAsync(
                user.Email,
                user.FirstName,
                user.VerificationToken.Value.ToString(),
                request.VerifyEmailUrl,
                cancellationToken);
            
            user.VerificationToken.UpdateLastSendOnUtc();
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        // Token ready to resend
        if (tokenStatus is TokenStatus.SentAndReadyToResend)
        {
            await _emailService.SendEmailWithVerificationTokenAsync(
                user.Email,
                user.FirstName,
                user.VerificationToken.Value.ToString(),
                request.VerifyEmailUrl,
                cancellationToken);
            
            user.VerificationToken.UpdateLastSendOnUtc();
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        return new ResendVerificationTokenResponse(
            tokenStatus,
            user.VerificationToken.GetTimeToResendToken());
    }
}