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