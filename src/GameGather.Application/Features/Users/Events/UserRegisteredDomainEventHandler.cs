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
//         var message = new EmailMessage(
//             "Verify your email",
//             "Welcome to GameGather",
//             $$"""
//                 <h1>Welcome to GameGather</h1>
//                 <p>Hi {{notification.FirstName}},</p>
//                 <p>Thank you for registering on GameGather. 
//                 Please verify your email address by pass this code: 
//                 {{notification.VerificationToken}}</p>
//                 Or click the button below to verify your email address:</p>
//                 <a href="{{notification.VerifyEmailUrl}}?email={{notification.Email}}&verificationCode={{notification.VerificationToken}}" 
//                    style="display: inline-block; background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;">
//                     Verify Email
//                 </a>
//                 """,
//             notification.Email);

        await _emailService.SendEmailWithVerificationTokenAsync(
            notification.Email,
            notification.FirstName,
            notification.VerificationToken.ToString(),
            notification.VerifyEmailUrl,
            cancellationToken);
    }
}