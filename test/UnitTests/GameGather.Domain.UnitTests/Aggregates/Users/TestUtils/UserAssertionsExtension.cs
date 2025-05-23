using FluentAssertions.Execution;
using GameGather.Domain.Aggregates.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.TestUtils;

public static class UserAssertionsExtension
{
    public static UserAssertions Should(this User instance)
        => new(instance);
}