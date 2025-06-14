using GameGather.Domain.DomainEvents;

namespace GameGather.UnitTests.Utils.Builders.ApplicationUsers.Events;

public class VerificationTokenRefreshedDomainEventBuilder
{
    private string _firstName = Constants.User.FirstName;
    private string _email = Constants.User.Email;
    private Guid _verificationToken = Constants.VerificationToken.Value;
    private string _verifyEmailUrl = Constants.User.VerifyEmailUrl;

    public VerificationTokenRefreshed Build()
    {
        return new VerificationTokenRefreshed(
            _firstName,
            _email,
            _verificationToken,
            _verifyEmailUrl);
    }
}