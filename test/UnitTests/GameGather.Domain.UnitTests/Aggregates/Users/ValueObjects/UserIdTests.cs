using FluentAssertions;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.UnitTests.TestUtils.Constants.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects;

public class UserIdTests
{
    [Fact]
    public void Create_Should_ReturnNewUserId_WhenCalled()
    {
        // Arrange
        
        // Act
        
        var userId = UserId.Create(Constants.UserId.Value);

        // Assert
        userId
            .Value
            .Should()
            .Be(Constants.UserId.Value);
    }
}