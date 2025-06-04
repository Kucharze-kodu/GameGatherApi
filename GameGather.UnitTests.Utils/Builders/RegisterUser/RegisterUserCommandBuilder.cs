using GameGather.Application.Features.Users.Commands.RegisterUser;

namespace GameGather.UnitTests.Utils.Builders.RegisterUser;

public class RegisterUserCommandBuilder
{
    private string _firstName = Constants.User.FirstName;
    private string _lastName = Constants.User.LastName;
    private string _email = Constants.User.Email;
    private string _password = Constants.User.PasswordValue;
    private string _confirmPassword = Constants.User.PasswordValue;
    private DateTime _birthday = Constants.User.Birthday;
    private string _verifyEmailUrl = Constants.User.VerifyEmailUrl;

    public RegisterUserCommandBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }
    
    public RegisterUserCommandBuilder WithFirstNameMinimum100CharactersLong()
    {
        _firstName = new string('A', 101);
        return this;
    }
    
    public RegisterUserCommandBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }
    
    public RegisterUserCommandBuilder WithLastNameMinimum100CharactersLong()
    {
        _lastName = new string('A', 101);
        return this;
    }
    
    public RegisterUserCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public RegisterUserCommandBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    
    public RegisterUserCommandBuilder WithConfirmPassword(string confirmPassword)
    {
        _confirmPassword = confirmPassword;
        return this;
    }
    
    public RegisterUserCommandBuilder WithBirthday(DateTime birthday)
    {
        _birthday = birthday;
        return this;
    }

    public RegisterUserCommand Build()
    {
        return new RegisterUserCommand(
            _firstName,
            _lastName,
            _email,
            _password,
            _confirmPassword,
            _birthday,
            _verifyEmailUrl);
    }
}