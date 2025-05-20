using System.Reflection;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.UnitTests.TestUtils.Constants.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects.TestUtils;

public class PasswordBuilder
{
    private string _value = Constants.Password.Value;
    private DateTime _lastModifiedOnUtc = Constants.Password.LastModifiedOnUtc;

    public PasswordBuilder WithExpiredPassword()
    {
        _lastModifiedOnUtc = DateTime.UtcNow.AddYears(-2);
        return this;
    }
    
    public Password Build()
    {
        var type = typeof(Password);
        
        var constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new[] { typeof(string) },
            null
        );
        
        if (constructor == null)
        {
            throw new InvalidOperationException("Constructor not found");
        }
        
        var password = (Password)constructor.Invoke(new object[] { _value });
        
        type.GetField("<LastModifiedOnUtc>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(password, _lastModifiedOnUtc);
        
        return password;
    }
}