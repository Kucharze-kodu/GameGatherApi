using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using BindingFlags = System.Reflection.BindingFlags;

namespace GameGather.UnitTests.Utils.Builders.DomainUsers;

public class ResetPasswordTokenBuilder
{
    private Guid _value = Constants.ResetPasswordToken.Value;
    private DateTime _createdOnUtc = Constants.ResetPasswordToken.CreatedOnUtc;
    private DateTime _expiresOnUtc = Constants.ResetPasswordToken.ExpiresOnUtc;
    private DateTime? _lastSendOnUtc = Constants.ResetPasswordToken.LastSendOnUtc;
    private DateTime? _usedOnUtc = Constants.ResetPasswordToken.UsedOnUtc;
    private TokenType _type = Constants.ResetPasswordToken.Type;

    public ResetPasswordTokenBuilder WithExpiredToken()
    {
        _expiresOnUtc = DateTime.UtcNow.AddDays(-1);
        return this;
    }
    
    public ResetPasswordTokenBuilder WithLastSendOnUtc(DateTime lastSendOnUtc)
    {
        _lastSendOnUtc = lastSendOnUtc;
        return this;
    }
    
    public ResetPasswordTokenBuilder WithNotLastSendOnUtc()
    {
        _lastSendOnUtc = null;
        return this;
    }
    
    public ResetPasswordTokenBuilder WithUsedToken()
    {
        _usedOnUtc = DateTime.UtcNow;
        return this;
    }

    public ResetPasswordTokenBuilder WithNotUsedOnUtc()
    {
        _usedOnUtc = null;
        return this;
    }
    
    public ResetPasswordTokenBuilder WithValue(Guid value)
    {
        _value = value;
        return this;
    }
    
    public ResetPasswordToken Build()
    {
        var type = typeof(ResetPasswordToken);
        
        var constructor = type.GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            Type.EmptyTypes,
            null);

        if (constructor == null)
        {
            throw new InvalidOperationException($"Constructor not found for {nameof(ResetPasswordToken)}");
        }
        
        var instance = (ResetPasswordToken)constructor.Invoke(null);
        
        type.GetField(
                "<Value>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _value);
        
        type.GetField(
                "<CreatedOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _createdOnUtc);
        
        type.GetField(
                "<ExpiresOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _expiresOnUtc);
        
        type.GetField(
                "<LastSendOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _lastSendOnUtc);
        
        type.GetField(
                "<UsedOnUtc>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _usedOnUtc);
        
        type.GetField(
                "<Type>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _type);
        
        return instance;
    }
}