using GameGather.Application.Features.Users.Commands.VerifyUser;

namespace GameGather.UnitTests.Utils.Builders.VerifyUser;

public class VerifyUserCommandBuilder
{
    private string _email = Constants.User.Email;
    private string _verificationCode = Constants.User.VerificationTokenValue.ToString();
    
    public VerifyUserCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public VerifyUserCommandBuilder WithVerificationCode(string verificationCode)
    {
        _verificationCode = verificationCode;
        return this;
    }
    
    public VerifyUserCommand Build()
    {
        return new VerifyUserCommand(
            _email,
            _verificationCode);
    }
}