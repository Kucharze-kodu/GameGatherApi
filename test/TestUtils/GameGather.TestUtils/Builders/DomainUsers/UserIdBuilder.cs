using System.Reflection;
using GameGather.Domain.Aggregates.Users.ValueObjects;

namespace GameGather.UnitTests.Utils.Builders.DomainUsers;

public class UserIdBuilder
{
    private int _value = Constants.UserId.Value;
    
    public UserIdBuilder WithValue(int value)
    {
        _value = value;
        return this;
    }

    public UserId Build()
    {
        var type = typeof(UserId);
        
        var constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new[] { typeof(int) },
            null
        );
        
        if (constructor == null)
        {
            throw new InvalidOperationException($"Constructor not found for {nameof(UserId)}");
        }
        
        var userId = (UserId)constructor.Invoke(new object[] { _value });
        
        type.GetField("<Value>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(userId, _value);
        
        return userId;
    }
}