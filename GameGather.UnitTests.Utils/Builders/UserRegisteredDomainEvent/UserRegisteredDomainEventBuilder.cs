using GameGather.Domain.DomainEvents;

namespace GameGather.UnitTests.Utils.Builders.UserRegisteredDomainEvent;

public class UserRegisteredDomainEventBuilder
{
    private string _firstName = Constants.User.FirstName;
    private string _lastName = Constants.User.LastName;
    private string _email = Constants.User.Email;
    private Guid _verificationToken = Constants.VerificationToken.Value;
    private string _verifyEmailUrl = Constants.User.VerifyEmailUrl;
    
    public UserRegistered Build()
    {
        return new UserRegistered(
            _firstName,
            _lastName,
            _email,
            _verificationToken,
            _verifyEmailUrl);
    }
}