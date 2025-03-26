using GameGather.Domain.Common.Primitives;

namespace GameGather.Domain.Aggregates.Users.ValueObjects;

public sealed class Password : ValueObject
{
    public string Value { get; private set; }
    public DateTime? ExpiresOnUtc { get; private set; }
    public DateTime LastModifiedOnUtc { get; private set; }

    private Password(string value)
    {
        Value = value;
        LastModifiedOnUtc = DateTime.UtcNow;
    }

    public static Password Create(string value) => new Password(value);

    public Password Load(
        string value,
        DateTime? expiresOnUtc,
        DateTime lastModifiedOnUtc
    )
    {
        Value = value;
        ExpiresOnUtc = expiresOnUtc;
        LastModifiedOnUtc = lastModifiedOnUtc;
        return this;
    }
    
    public bool IsExpired(int days) => LastModifiedOnUtc.AddDays(days) < DateTime.UtcNow;
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return ExpiresOnUtc;
        yield return LastModifiedOnUtc;
    }
}