using GameGather.Application.Persistance;
using GameGather.Application.Utils.Email;
using GameGather.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace GameGather.Application.Features.Users.Events;

public sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegistered>
{
    private readonly IEmailService _emailService;

    public UserRegisteredDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailWithVerificationTokenAsync(
            notification.Email,
            notification.FirstName,
            notification.VerificationToken.ToString(),
            notification.VerifyEmailUrl,
            cancellationToken);
    }
}