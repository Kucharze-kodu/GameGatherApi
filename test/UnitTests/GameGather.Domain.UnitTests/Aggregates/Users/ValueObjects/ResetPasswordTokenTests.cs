using FluentAssertions;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects.TestUtils;
using GameGather.Domain.UnitTests.TestUtils.Constants.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects;

public class ResetPasswordTokenTests
{
    [Fact]
    public void Create_ShouldReturnNewResetPasswordToken_WhenCalled()
    {
        // Arrange
        
        var value = new ResetPasswordTokenBuilder()
            .Build()
            .Value;
        
        // Act
        
        var resetPasswordToken = ResetPasswordToken.Create();
        
        // Assert
        
        resetPasswordToken
            .Value
            .Should()
            .NotBe(value);
        resetPasswordToken
            .CreatedOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow,
                TimeSpan.FromMinutes(Constants.ResetPasswordToken.MaxDifferenceInMinutes));
        resetPasswordToken
            .ExpiresOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow.AddDays(Constants.ResetPasswordToken.TokenValidityInDays),
                TimeSpan.FromMinutes(Constants.ResetPasswordToken.MaxDifferenceInMinutes));
        resetPasswordToken
            .LastSendOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow,
                TimeSpan.FromMinutes(Constants.ResetPasswordToken.MaxDifferenceInMinutes));
        resetPasswordToken
            .Type
            .Should()
            .Be(Constants.ResetPasswordToken.Type);
    }
    
    [Fact]
    public void Load_ShouldReturnLoadedResetPasswordToken_WhenValidValuesAreProvided()
    {
        // Arrange
        
        var resetPasswordToken = ResetPasswordToken.Create();
        var resetPasswordTokenToLoad = new ResetPasswordTokenBuilder().Build();
        
        // Act
        
        resetPasswordToken.Load(
            resetPasswordTokenToLoad.Value,
            resetPasswordTokenToLoad.CreatedOnUtc,
            resetPasswordTokenToLoad.ExpiresOnUtc,
            resetPasswordTokenToLoad.LastSendOnUtc,
            resetPasswordTokenToLoad.UsedOnUtc,
            resetPasswordTokenToLoad.Type);
        
        // Assert
        
        resetPasswordToken
            .Value
            .Should()
            .Be(resetPasswordTokenToLoad.Value);
        resetPasswordToken
            .CreatedOnUtc
            .Should()
            .Be(resetPasswordTokenToLoad.CreatedOnUtc);
        resetPasswordToken
            .ExpiresOnUtc
            .Should()
            .Be(resetPasswordTokenToLoad.ExpiresOnUtc);
        resetPasswordToken
            .LastSendOnUtc
            .Should()
            .Be(resetPasswordTokenToLoad.LastSendOnUtc);
        resetPasswordToken
            .UsedOnUtc
            .Should()
            .Be(resetPasswordTokenToLoad.UsedOnUtc);
        resetPasswordToken
            .Type
            .Should()
            .Be(resetPasswordTokenToLoad.Type);
    }
    
    [Fact]
    public void Verify_ShouldReturnFalse_WhenTokenIsNotValid()
    {
        // Arrange
        
        var resetPasswordToken = new ResetPasswordTokenBuilder().Build();
        var tokenValue = Guid.NewGuid();
        
        // Act
        
        var isValid = resetPasswordToken.Verify(tokenValue);
        
        // Assert
        
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void Verify_ShouldReturnFalse_WhenTokenIsExpired()
    {
        // Arrange
        
        var resetPasswordToken = new ResetPasswordTokenBuilder()
            .WithExpiredToken()
            .Build();
        
        // Act
        
        var isValid = resetPasswordToken.Verify(resetPasswordToken.Value);
        
        // Assert
        
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void Verify_ShouldUpdateUsedOnUtcAndReturnTrue_WhenTokenIsValidAndNotExpired()
    {
        // Arrange 
        
        var resetPasswordToken = new ResetPasswordTokenBuilder().Build();
        
        // Act
        
        var isValid = resetPasswordToken.Verify(resetPasswordToken.Value);
        
        // Assert
        
        isValid
            .Should()
            .BeTrue();
        resetPasswordToken
            .UsedOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow,
                TimeSpan.FromMinutes(Constants.ResetPasswordToken.MaxDifferenceInMinutes));
    }
    
    [Fact]
    public void CheckStatus_ShouldReturnTokenNotReadyToResend_WhenTokenWasAlreadySent()
    {
        // Arrange
        
        var resetPasswordToken = new ResetPasswordTokenBuilder()
            .WithLastSendOnUtc(DateTime
                .UtcNow
                .AddMinutes(Constants.ResetPasswordToken.MaxDifferenceInMinutes - 2))
            .Build();
        
        // Act
        
        var status = resetPasswordToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenNotReadyToResend);
    }
    
    [Fact]
    public void CheckStatus_ShouldReturnTokenExpired_WhenTokenIsExpired()
    {
        // Arrange
        
        var resetPasswordToken = new ResetPasswordTokenBuilder()
            .WithExpiredToken()
            .Build();
        
        // Act
        
        var status = resetPasswordToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenExpired);
    }
    
    [Fact]
    public void CheckStatus_ShouldReturnTokenUsed_WhenTokenIsUsed()
    {
        // Arrange
        
        var resetPasswordToken = new ResetPasswordTokenBuilder()
            .WithUsedToken()
            .Build();
        
        // Act
        
        var status = resetPasswordToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenUsed);
    }
    
    [Fact]
    public void CheckStatus_ShouldReturnTokenReadyToResend_WhenTokenIsNotAlreadySentAndIsNotExpiredAndNotUsed()
    {
        // Arrange
        
        var resetPasswordToken = new ResetPasswordTokenBuilder()
            .WithNotUsedOnUtc()
            .Build();
        
        // Act
        
        var status = resetPasswordToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenReadyToResend);
    }
}