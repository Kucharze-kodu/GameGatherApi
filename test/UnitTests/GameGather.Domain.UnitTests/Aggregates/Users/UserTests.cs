using FluentAssertions;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.DomainEvents;
using GameGather.Domain.UnitTests.Aggregates.Users.TestUtils;
using GameGather.UnitTests.Utils;
using GameGather.UnitTests.Utils.Builders.Users;

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
    
    [Fact]
    public void Load_Should_ReturnLoadedUser_WhenValidParametersAreProvided()
    {
        // Arrange

        var userToLoad = new UserBuilder()
            .Build();
        var user = new UserBuilder().EmptyObject();
        // var user = User.Create(
        //     "John",
        //     "Doe",
        //     "john.doe@gmail.com",
        //     "password123",
        //     DateTime.UtcNow.AddYears(-20),
        //     "https://example.com/verify-email"
        // );
        // user.ClearDomainEvents();
            

        // Act

        user.Load(
            userToLoad.Id,
            userToLoad.FirstName,
            userToLoad.LastName,
            userToLoad.Email,
            userToLoad.Password,
            userToLoad.Birthday,
            userToLoad.CreatedOnUtc,
            userToLoad.VerifiedOnUtc,
            userToLoad.LastModifiedOnUtc,
            userToLoad.Ban,
            userToLoad.Role,
            userToLoad.VerificationToken,
            userToLoad.ResetPasswordToken
        );

        // Assert

        user
            .Id
            .Should()
            .Be(userToLoad.Id);
        user
            .FirstName
            .Should()
            .Be(userToLoad.FirstName);
        user
            .LastName
            .Should()
            .Be(userToLoad.LastName);
        user
            .Email
            .Should()
            .Be(userToLoad.Email);
        user
            .Password
            .Should()
            .Be(userToLoad.Password);
        user
            .Birthday
            .Should()
            .Be(userToLoad.Birthday);
        user
            .CreatedOnUtc
            .Should()
            .Be(userToLoad.CreatedOnUtc);
        user
            .VerifiedOnUtc
            .Should()
            .Be(userToLoad.VerifiedOnUtc);
        user
            .LastModifiedOnUtc
            .Should()
            .Be(userToLoad.LastModifiedOnUtc);
        user
            .VerificationToken
            .Should()
            .Be(userToLoad.VerificationToken);
        user
            .ResetPasswordToken
            .Should()
            .Be(userToLoad.ResetPasswordToken);
        user
            .Ban
            .Should()
            .Be(userToLoad.Ban);
        user
            .Role
            .Should()
            .Be(userToLoad.Role);
    }
    
    [Fact]
    public void IsVerified_Should_ReturnTrue_WhenUserIsVerified()
    {
        // Arrange

        var user = new UserBuilder().Build();

        // Act

        var isVerified = user.IsVerified;

        // Assert

        isVerified
            .Should()
            .BeTrue();
    }
    
    [Fact]
    public void IsVerified_Should_ReturnFalse_WhenUserIsNotVerified()
    {
        // Arrange

        var user = new UserBuilder().EmptyObject();

        // Act

        var isVerified = user.IsVerified;

        // Assert

        isVerified
            .Should()
            .BeFalse();
    }
    
    [Fact]
    public void Verify_Should_ReturnFalse_WhenUserIsAlreadyVerified()
    {
        // Arrange

        var user = new UserBuilder().Build();
        var token = user.VerificationToken.Value;

        // Act

        var isVerified = user.Verify(token);

        // Assert

        isVerified
            .Should()
            .BeFalse();
        user
            .VerifiedOnUtc
            .Should()
            .NotBeNull();
    }
    
    [Fact]
    public void Verify_Should_ReturnFalse_WhenTokenIsInvalid()
    {
        // Arrange

        var user = new UserBuilder().Build();
        var invalidToken = Guid.NewGuid();

        // Act

        var isVerified = user.Verify(invalidToken);

        // Assert

        isVerified
            .Should()
            .BeFalse();
        user
            .VerifiedOnUtc
            .Should()
            .NotBeNull();
    }
    
    [Fact]
    public void Verify_Should_ReturnTrue_WhenTokenIsValidAndUserIsNotVerified()
    {
        // Arrange

        var user = new UserBuilder()
            .NotVerified()
            .Build();
        var token = user.VerificationToken.Value;

        // Act

        var isVerified = user.Verify(token);

        // Assert

        isVerified
            .Should()
            .BeTrue();
        user
            .VerifiedOnUtc
            .Should()
            .NotBeNull();
        user
            .LastModifiedOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow,
                TimeSpan.FromMinutes(Constants.User.MaxDifferenceInMinutes));
    }
    
    [Fact]
    public void GenerateVerificationToken_Should_GenerateNewVerificationToken_WhenCalled()
    {
        // Arrange

        var user = new UserBuilder().Build();
        var token = user.VerificationToken;

        // Act

        user.GenerateNewVerificationToken(Constants.User.VerifyEmailUrl);

        // Assert

        user
            .VerificationToken
            .Should()
            .NotBeEquivalentTo(token);
        user
            .DomainEvents
            .Should()
            .NotBeNullOrEmpty();
        user
            .DomainEvents
            .Should()
            .Contain(r => r is VerificationTokenRefreshed);
    }
}