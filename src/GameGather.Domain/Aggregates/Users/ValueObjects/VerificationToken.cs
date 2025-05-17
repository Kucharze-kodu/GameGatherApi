using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Common.Interfaces;
using GameGather.Domain.Common.Primitives;
using Newtonsoft.Json;

namespace GameGather.Domain.Aggregates.Users.ValueObjects;

public sealed class VerificationToken : ValueObject, IToken
{
    public Guid Value { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime ExpiresOnUtc { get; private set; }
    public DateTime LastSendOnUtc { get; private set; }
    public DateTime? UsedOnUtc { get; private set; }
    public TokenType Type { get; private set; }

    [JsonConstructor]
    private VerificationToken()
    {
        Value = Guid.NewGuid();
        CreatedOnUtc = DateTime.UtcNow;
        ExpiresOnUtc = DateTime.UtcNow.AddDays(1);
        LastSendOnUtc = DateTime.UtcNow;
        Type = TokenType.VerificationToken;
    }

    public static VerificationToken Create() => new VerificationToken();

    public VerificationToken Load(
        Guid value,
        DateTime createdOnUtc,
        DateTime expiresOnUtc,
        DateTime lastSendOnUtc,
        DateTime? usedOnUtc,
        TokenType type
    )
    {
        Value = value;
        CreatedOnUtc = createdOnUtc;
        ExpiresOnUtc = expiresOnUtc;
        LastSendOnUtc = lastSendOnUtc;
        UsedOnUtc = usedOnUtc;
        Type = type;
        return this;
    }
    
    public bool Verify(Guid token)
    {
        // Check if the token is valid
        if (Value != token)
        {
            return false;
        }

        // Check if the token is expired
        if (ExpiresOnUtc.CompareTo(DateTime.UtcNow) != 1)
        {
            return false;
        }

        UsedOnUtc = DateTime.UtcNow;
        return true;
    }
    
    public TokenStatus CheckStatus()
    {
        // Check if the token was already sent
        if (LastSendOnUtc.AddMinutes(5).CompareTo(DateTime.UtcNow) != -1)
        {
            return TokenStatus.TokenAlreadySent;
        }
        
        // Check if the token is expired
        if (ExpiresOnUtc.CompareTo(DateTime.UtcNow) != 1)
        {
            return TokenStatus.TokenExpired;
        }
        
        // Check if the token was already used
        if (UsedOnUtc is not null)
        {
            return TokenStatus.TokenUsed;
        }

        return TokenStatus.TokenReadyToResend;
    }
    
    public TimeOnly GetTimeToResendToken()
    {
        var timeToResend = LastSendOnUtc.AddMinutes(5);
        return TimeOnly.FromDateTime(timeToResend);
    }

    public void UpdateLastSendOnUtc() => LastSendOnUtc = DateTime.UtcNow;
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return CreatedOnUtc;
        yield return ExpiresOnUtc;
        yield return LastSendOnUtc;
        yield return UsedOnUtc;
        yield return Type;
    }
}