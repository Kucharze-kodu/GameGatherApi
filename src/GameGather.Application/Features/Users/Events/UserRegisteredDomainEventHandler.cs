using GameGather.Application.Persistance;
using GameGather.Application.Utils.Email;
using GameGather.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace GameGather.Application.Features.Users.Events;

public sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegistered>
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserRegisteredDomainEventHandler(
        IEmailService emailService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _emailService = emailService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(notification.Email, cancellationToken);
        
        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        await _emailService.SendEmailWithVerificationTokenAsync(
            notification.Email,
            notification.FirstName,
            notification.VerificationToken.ToString(),
            notification.VerifyEmailUrl,
            cancellationToken);
        
        user.VerificationToken.UpdateLastSendOnUtc();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}