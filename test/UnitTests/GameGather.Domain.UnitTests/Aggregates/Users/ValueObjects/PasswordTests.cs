using FluentAssertions;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects.TestUtils;
using GameGather.Domain.UnitTests.TestUtils.Constants;
using Constants = GameGather.Domain.UnitTests.TestUtils.Constants.Users.Constants;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void Create_Should_ReturnNewPassword_WhenValidValueIsProvided()
    {
        // Arrange

        var value = new PasswordBuilder().Build().Value;

        // Act
        
        var password = Password.Create(value);

        // Assert
        
        password.Value.Should().Be(value);
        password.LastModifiedOnUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(5));
    }
    
    [Fact]
    public void Load_Should_ReturnLoadedPassword_WhenValidValuesAreProvided()
    {
        // Arrange
        
        var passwordToLoad = new PasswordBuilder().Build();
        var password = Password.Create("password");
        
        // Act
        
        password.Load(
            passwordToLoad.Value,
            passwordToLoad.LastModifiedOnUtc
        );

        // Assert
        
        password.Value.Should().Be(passwordToLoad.Value);
        password.LastModifiedOnUtc.Should().Be(passwordToLoad.LastModifiedOnUtc);
    }

    [Fact]
    public void IsExpired_Should_ReturnTrue_WhenPasswordIsExpired()
    {
        // Arrange
        var password = new PasswordBuilder()
            .WithExpiredPassword()
            .Build();
        
        // Act
        
        var isExpired = password.IsExpired(30);
        
        // Assert
        
        isExpired.Should().BeTrue();
    }

    [Fact]
    public void IsExpired_Should_ReturnFalse_WhenPasswordIsNotExpired()
    {
        // Arrange
        var password = new PasswordBuilder()
            .Build();
        
        // Act
        
        var isExpired = password.IsExpired(30);
        
        // Assert
        
        isExpired.Should().BeFalse();
    }
}