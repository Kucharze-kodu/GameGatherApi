using System.Reflection;
using System.Runtime.Serialization;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects.TestUtils;
using GameGather.Domain.UnitTests.TestUtils.Constants.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.TestUtils;

public class UserBuilder
{
    private UserId _userId = new UserIdBuilder()
        .WithValue(Constants.User.UserIdValue)
        .Build();
    private string _firstName = Constants.User.FirstName;
    private string _lastName = Constants.User.LastName;
    private string _email = Constants.User.Email;
    private Password _password = new PasswordBuilder()
        .WithValue(Constants.User.PasswordValue)
        .Build();
    private DateTime _birthday = Constants.User.Birthday;
    private DateTime _createdOnUtc = Constants.User.CreatedOnUtc;
    private DateTime? _verifiedOnUtc = Constants.User.VerifiedOnUtc;
    private DateTime _lastModifiedOnUtc = Constants.User.LastModifiedOnUtc;
    private VerificationToken _verificationToken = new VerificationTokenBuilder()
        .WithValue(Constants.User.VerificationTokenValue)
        .Build();
    private ResetPasswordToken? _resetPasswordToken = new ResetPasswordTokenBuilder()
        .WithValue(Constants.User.ResetPasswordTokenValue)
        .Build();
    private Ban? _ban = null;
    private Role _role = Constants.User.Role;
    
    public UserBuilder NotVerified()
    {
        _verifiedOnUtc = null;
        return this;
    }

    public User EmptyObject()
    {
        return (User)FormatterServices.GetUninitializedObject(typeof(User));
    }

    public User Build()
    {
        var type = typeof(User);
        
        var constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new[]
            {
                typeof(UserId),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(DateTime)
            },
            null
        );
        
        if (constructor == null)
        {
            throw new InvalidOperationException($"Constructor not found for {nameof(User)}");
        }
        
        var instance = (User)constructor.Invoke(new object[]
        {
            _userId,
            _firstName,
            _lastName,
            _email,
            _password.Value,
            _birthday
        });
        
        type.GetField(
                "<Id>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _userId);
        
        type.GetField(
                "<FirstName>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _firstName);
        
        type.GetField(
                "<LastName>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _lastName);
        
        type.GetField(
                "<Email>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _email);
        
        type.GetField(
                "<Password>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _password);
        
        type.GetField(
                "<Birthday>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _birthday);
        
        type.GetField(
                "<CreatedOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _createdOnUtc);
        
        type.GetField(
                "<VerifiedOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _verifiedOnUtc);
        
        type.GetField(
                "<LastModifiedOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _lastModifiedOnUtc);
        
        type.GetField(
                "<VerificationToken>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _verificationToken);
        
        type.GetField(
                "<ResetPasswordToken>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _resetPasswordToken);
        
        type.GetField(
                "<Ban>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _ban);
        
        type.GetField(
            "<Role>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _role);
        
        return instance;
    }
}