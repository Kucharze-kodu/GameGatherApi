using GameGather.Application.Features.Users.Commands.LoginUser;
using GameGather.IntegrationTests.TestUtils;

namespace GameGather.IntegrationTests.Users.TestUtils;

public class LoginUserCommandBuilder
{
    private string _email = Constants.LoginUser.Email;
    private string _password = Constants.LoginUser.Password;
    
    public static LoginUserCommandBuilder GivenLoginUserCommand() =>
        new LoginUserCommandBuilder();
    
    public LoginUserCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public LoginUserCommandBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    
    public LoginUserCommandBuilder WithExpiredPassword()
    {
        _email = Constants.LoginUserWithExpiredPassword.Email;
        _password = Constants.LoginUserWithExpiredPassword.Password;
        return this;
    }
    
    public LoginUserCommandBuilder WithNotVerifiedEmail()
    {
        _email = Constants.LoginUserWithNotVerifiedEmail.Email;
        _password = Constants.LoginUserWithNotVerifiedEmail.Password;
        return this;
    }
    
    public LoginUserCommand Build()
    {
        return new LoginUserCommand(
            _email,
            _password);
    }
}