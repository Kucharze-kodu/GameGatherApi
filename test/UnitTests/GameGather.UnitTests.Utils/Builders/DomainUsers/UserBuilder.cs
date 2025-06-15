using System.Reflection;
using System.Runtime.Serialization;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;

namespace GameGather.UnitTests.Utils.Builders.DomainUsers;

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

    public UserBuilder WithUserId(UserId userId)
    {
        _userId = userId;
        return this;
    }
    
    public UserBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }
    
    public UserBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }
    
    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public UserBuilder WithPassword(Password password)
    {
        _password = password;
        return this;
    }
    
    public UserBuilder WithBirthday(DateTime birthday)
    {
        _birthday = birthday;
        return this;
    }
    
    public UserBuilder WithCreatedOnUtc(DateTime createdOnUtc)
    {
        _createdOnUtc = createdOnUtc;
        return this;
    }
    
    public UserBuilder WithVerifiedOnUtc(DateTime? verifiedOnUtc)
    {
        _verifiedOnUtc = verifiedOnUtc;
        return this;
    }
    
    public UserBuilder WithLastModifiedOnUtc(DateTime lastModifiedOnUtc)
    {
        _lastModifiedOnUtc = lastModifiedOnUtc;
        return this;
    }
    
    public UserBuilder WithVerificationToken(VerificationToken verificationToken)
    {
        _verificationToken = verificationToken;
        return this;
    }
    
    public UserBuilder WithResetPasswordToken(ResetPasswordToken? resetPasswordToken)
    {
        _resetPasswordToken = resetPasswordToken;
        return this;
    }
    
    public UserBuilder WithBan(Ban? ban)
    {
        _ban = ban;
        return this;
    }
    
    public UserBuilder WithRole(Role role)
    {
        _role = role;
        return this;
    }
    
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