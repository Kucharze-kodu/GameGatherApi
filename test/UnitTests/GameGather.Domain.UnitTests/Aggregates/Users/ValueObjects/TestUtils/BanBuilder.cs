using System.Reflection;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.UnitTests.TestUtils.Constants.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects.TestUtils;

public class BanBuilder
{
    private DateTime _createdOnUtc = Constants.Ban.CreatedOnUtc;
    private DateTime? _expiresOnUtc = Constants.Ban.ExpiresOnUtc;
    private string _message = Constants.Ban.Message;

    
    public Ban Build()
    {
        var type = typeof(Ban);
        
        var constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new[] { typeof(string), typeof(DateTime?) },
            null
        );
        
        if (constructor == null)
        {
            throw new InvalidOperationException("Constructor not found.");
        }
        
        var instance = (Ban)constructor.Invoke(new object[] { _message, _expiresOnUtc });
        
        type.GetField(
                "<CreatedOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _createdOnUtc);
        
        return instance;
    }
}