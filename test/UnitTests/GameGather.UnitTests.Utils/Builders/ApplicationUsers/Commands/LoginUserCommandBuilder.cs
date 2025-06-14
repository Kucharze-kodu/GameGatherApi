using GameGather.Application.Features.Users.Commands.LoginUser;

namespace GameGather.UnitTests.Utils.Builders.ApplicationUsers.Commands;

public class LoginUserCommandBuilder
{
    private string _email = Constants.User.Email;
    private string _password = Constants.User.PasswordValue;
    
    public LoginUserCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public LoginUserCommandBuilder WithEmptyEmail()
    {
        _email = string.Empty;
        return this;
    }
    
    public LoginUserCommandBuilder WithWrongEmail()
    {
        _email = Constants.LoginUserCommand.Email;
        return this;
    }
    
    public LoginUserCommandBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    
    public LoginUserCommandBuilder WithEmptyPassword()
    {
        _password = string.Empty;
        return this;
    }
    
    public LoginUserCommandBuilder WithWrongPassword()
    {
        _password = Constants.LoginUserCommand.Password;
        return this;
    }

    public LoginUserCommand Build()
    {
        return new LoginUserCommand(_email, _password);
    }
}