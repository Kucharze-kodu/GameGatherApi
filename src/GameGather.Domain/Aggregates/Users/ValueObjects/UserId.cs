using GameGather.Domain.Common.Primitives;

namespace GameGather.Domain.Aggregates.Users.ValueObjects;

public sealed class UserId : ValueObject
{
    public int Value { get; }

    private UserId(int value)
    {
        Value = value;
    }

    public static UserId Create(int id) => new UserId(id);
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}