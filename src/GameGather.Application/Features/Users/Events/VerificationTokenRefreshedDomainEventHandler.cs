using GameGather.Application.Utils.Email;
using GameGather.Domain.DomainEvents;
using MediatR;

namespace GameGather.Application.Features.Users.Events;

public class VerificationTokenRefreshedDomainEventHandler : INotificationHandler<VerificationTokenRefreshed>
{
    private readonly IEmailService _emailService;
    
    public VerificationTokenRefreshedDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(VerificationTokenRefreshed notification, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailWithVerificationTokenAsync(
            notification.Email,
            notification.FirstName,
            notification.VerificationToken.ToString(),
            notification.VerifyEmailUrl,
            cancellationToken);
    }
}