using GameGather.Application.Persistance;
using GameGather.Application.Utils.Email;
using GameGather.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace GameGather.Application.Features.Users.Events;

public sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegistered>
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public UserRegisteredDomainEventHandler(
        IEmailService emailService, 
        IConfiguration configuration, 
        IUserRepository userRepository)
    {
        _emailService = emailService;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        var baseURl = _configuration["Url:Backend"];
        var userId = await _userRepository.GetIdByEmailAsync(notification.Email, cancellationToken);
        
        if (userId is null)
        {
            throw new ApplicationException("User not found");
        }
        
        var verifyUserEndpoint = $"{baseURl}/api/verify?userId={userId}&token={notification.VerificationToken}";
        var message = new EmailMessage(
            "Verify your email",
            "Welcome to GameGather",
            $$"""
                <h1>Welcome to GameGather</h1>
                <p>Hi {{notification.FirstName}},</p>
                <p>Thank you for registering on GameGather. 
                Please verify your email address by pass this code: 
                {{notification.VerificationToken}}</p>
                Or click the button below to verify your email address:</p>
                <form action="{{ verifyUserEndpoint }}" method="POST">
                    <button type="submit" style="background-color: #4CAF50; color: white; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer;">
                        Verify Email
                    </button>
                </form>
                """,
            notification.Email);
        
        await _emailService.SendEmailAsync(message);
    }
}