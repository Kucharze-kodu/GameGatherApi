using FluentAssertions;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.DomainEvents;
using GameGather.Domain.UnitTests.Aggregates.Users.TestUtils;
using GameGather.Domain.UnitTests.TestUtils.Constants.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users;

public class UserTests
{
    [Fact]
    public void CreateUser_ShouldCreateUserAndRaiseDomainEvent_WhenValidParametersAreProvided()
    {
        // Arrange


        // Act

        var user = User.Create(
            Constants.User.FirstName,
            Constants.User.LastName,
            Constants.User.Email,
            Constants.User.PasswordValue,
            Constants.User.Birthday,
            Constants.User.VerifyEmailUrl
        );

        // Assert

        user
            .Should()
            .BeValidAfterCreation();
        user
            .DomainEvents
            .Should()
            .NotBeEmpty();
        user
            .DomainEvents
            .Should()
            .Contain(r => r is UserRegistered);
    }
}