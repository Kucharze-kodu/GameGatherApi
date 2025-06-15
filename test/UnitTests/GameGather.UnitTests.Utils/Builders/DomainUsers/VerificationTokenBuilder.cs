using System.Reflection;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;

namespace GameGather.UnitTests.Utils.Builders.DomainUsers;

public class VerificationTokenBuilder
{
    private Guid _value = Constants.VerificationToken.Value;
    private DateTime _createdOnUtc = Constants.VerificationToken.CreatedOnUtc;
    private DateTime _expiresOnUtc = Constants.VerificationToken.ExpiresOnUtc;
    private DateTime? _lastSendOnUtc = Constants.VerificationToken.LastSendOnUtc;
    private DateTime? _usedOnUtc = Constants.VerificationToken.UsedOnUtc;
    private TokenType _type = Constants.VerificationToken.Type;

    public VerificationTokenBuilder WithValue(Guid value)
    {
        _value = value;
        return this;
    }
    
    public VerificationTokenBuilder WithCreatedOnUtc(DateTime createdOnUtc)
    {
        _createdOnUtc = createdOnUtc;
        return this;
    }
    
    public VerificationTokenBuilder WithExpiresOnUtc(DateTime expiresOnUtc)
    {
        _expiresOnUtc = expiresOnUtc;
        return this;
    }
    
    public VerificationTokenBuilder WithLastSendOnUtc(DateTime lastSendOnUtc)
    {
        _lastSendOnUtc = lastSendOnUtc;
        return this;
    }
    
    public VerificationTokenBuilder WithUsedOnUtc(DateTime? usedOnUtc)
    {
        _usedOnUtc = usedOnUtc;
        return this;
    }
    
    public VerificationTokenBuilder WithExpiredToken()
    {
        _expiresOnUtc = DateTime.UtcNow.AddDays(-2);
        return this;
    }
    
    public VerificationTokenBuilder WithUsedToken()
    {
        _usedOnUtc = DateTime.UtcNow;
        return this;
    }
    
    
    
    public VerificationTokenBuilder WithNotLastSendOnUtc()
    {
        _lastSendOnUtc = null;
        return this;
    }
    
    public VerificationTokenBuilder WithNotUsedOnUtc()
    {
        _usedOnUtc = null;
        return this;
    }
    
    
    
    public VerificationToken Build()
    {
        var type = typeof(VerificationToken);
        var instance = Activator.CreateInstance(type, nonPublic: true);

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

        type.GetField("<Type>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic)?
            .SetValue(instance, _type);

        return (VerificationToken)instance!;
    }
}