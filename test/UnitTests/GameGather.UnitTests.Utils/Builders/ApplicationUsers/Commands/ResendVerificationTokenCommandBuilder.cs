using GameGather.Application.Features.Users.Commands.ResendVerificationToken;

namespace GameGather.UnitTests.Utils.Builders.ApplicationUsers.Commands;

public class ResendVerificationTokenCommandBuilder
{
    private string _email = Constants.User.Email;
    private string _verifyEmailUrl = Constants.User.VerifyEmailUrl;
    
    public ResendVerificationTokenCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public ResendVerificationTokenCommandBuilder WithVerifyEmailUrl(string verifyEmailUrl)
    {
        _verifyEmailUrl = verifyEmailUrl;
        return this;
    }
    
    public ResendVerificationTokenCommand Build()
    {
        return new ResendVerificationTokenCommand(
            _email,
            _verifyEmailUrl);
    }
}