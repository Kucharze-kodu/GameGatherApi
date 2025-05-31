using FluentAssertions;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.UnitTests.Utils;
using GameGather.UnitTests.Utils.Builders.Users;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects;

public class VerificationTokenTests
{
    [Fact]
    public void Create_Should_ReturnNewVerificationToken_WhenCalled()
    {
        // Arrange
        var value = new VerificationTokenBuilder()
            .Build()
            .Value;
        
        
        // Act
        
        var verificationToken = VerificationToken.Create();

        // Assert
        
        verificationToken
            .Value
            .Should()
            .NotBe(value);
        verificationToken
            .CreatedOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow, 
                TimeSpan.FromMinutes(Constants.VerificationToken.MaxDifferenceInMinutes));
        verificationToken
            .ExpiresOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow.AddDays(Constants.VerificationToken.TokenValidityInDays),
                TimeSpan.FromMinutes(Constants.VerificationToken.MaxDifferenceInMinutes));
        verificationToken
            .LastSendOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow, 
                TimeSpan.FromMinutes(Constants.VerificationToken.MaxDifferenceInMinutes));
        verificationToken
            .Type
            .Should()
            .Be(Constants.VerificationToken.Type);
    }
    
    [Fact]
    public void Load_Should_ReturnLoadedVerificationToken_WhenValidValuesAreProvided()
    {
        // Arrange
        
        var verificationToken = VerificationToken.Create();
        var verificationTokenToLoad = new VerificationTokenBuilder().Build();

        // Act
        
        verificationToken.Load(
            verificationTokenToLoad.Value,
            verificationTokenToLoad.CreatedOnUtc,
            verificationTokenToLoad.ExpiresOnUtc,
            verificationTokenToLoad.LastSendOnUtc,
            verificationTokenToLoad.UsedOnUtc,
            verificationTokenToLoad.Type);

        // Assert
        
        verificationToken
            .Value
            .Should()
            .Be(verificationTokenToLoad.Value);
        verificationToken
            .CreatedOnUtc
            .Should().
            Be(verificationTokenToLoad.CreatedOnUtc);
        verificationToken
            .ExpiresOnUtc
            .Should()
            .Be(verificationTokenToLoad.ExpiresOnUtc);
        verificationToken
            .LastSendOnUtc
            .Should()
            .Be(verificationTokenToLoad.LastSendOnUtc);
        verificationToken
            .UsedOnUtc
            .Should()
            .Be(verificationTokenToLoad.UsedOnUtc);
        verificationToken
            .Type
            .Should()
            .Be(verificationTokenToLoad.Type);
    }
    
    [Fact]
    public void Verify_Should_ReturnFalse_WhenTokenIsNotValid()
    {
        // Arrange

        var verificationToken = new VerificationTokenBuilder().Build();
        var tokenValue = Guid.NewGuid();

        // Act
        
        var isValid = verificationToken.Verify(tokenValue);

        // Assert
        
        isValid
            .Should()
            .BeFalse();
    }

    [Fact]
    public void Verify_Should_ReturnFalse_WhenTokenIsExpired()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder()
            .WithExpiredToken()
            .Build();
        
        // Act
        
        var isValid = verificationToken.Verify(verificationToken.Value);
        
        // Assert
        
        isValid
            .Should()
            .BeFalse();
    }

    [Fact]
    public void Verify_Should_UpdateUsedOnUtcAndReturnTrue_WhenTokenIsValidAndNotExpired()
    {
        // Arrange 
        
        var verificationToken = new VerificationTokenBuilder().Build();
        
        // Act
        
        var isValid = verificationToken.Verify(verificationToken.Value);
        
        // Assert
        
        verificationToken
            .UsedOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow, 
                TimeSpan.FromMinutes(Constants.VerificationToken.MaxDifferenceInMinutes));
        isValid
            .Should()
            .BeTrue();
    } 
    
    [Fact]
    public void CheckStatus_Should_ReturnTokenNotReadyToResend_WhenTokenWasAlreadySent()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder()
            .WithLastSendOnUtc(DateTime.UtcNow)
            .Build();
        
        // Act
        
        var status = verificationToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenNotReadyToResend);
    }
    
    [Fact]
    public void CheckStatus_Should_ReturnTokenExpired_WhenTokenIsExpired()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder()
            .WithExpiredToken()
            .Build();
        
        // Act
        
        var status = verificationToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenExpired);
    }
    
    [Fact]
    public void CheckStatus_Should_ReturnTokenUsed_WhenTokenWasAlreadyUsed()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder()
            .WithUsedToken()
            .Build();
        
        // Act
        
        var status = verificationToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenUsed);
    }
    
    [Fact]
    public void CheckStatus_Should_ReturnTokenReadyToResend_WhenTokenWasNotAlreadySentAndNotExpiredAndNotUsed()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder()
            .WithNotUsedOnUtc()
            .Build();
        
        // Act
        
        var status = verificationToken.CheckStatus();
        
        // Assert
        
        status
            .Should()
            .Be(TokenStatus.TokenReadyToResend);
    }
    
    [Fact]
    public void GetTimeToResendToken_Should_ReturnTimeToResend()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder()
            .WithLastSendOnUtc(DateTime.UtcNow)
            .Build();
        
        // Act
        
        var timeToResend = verificationToken.GetTimeToResendToken();
        
        // Assert
        
        timeToResend
            .Should()
            .BeCloseTo(
                TimeSpan.FromMinutes(Constants.VerificationToken.MinimumTimeToResendInMinutes),
                TimeSpan.FromMinutes(Constants.VerificationToken.MaxDifferenceInMinutes));
    }
    
    [Fact]
    public void UpdateLastSendOnUtc_Should_UpdateLastSendOnUtc()
    {
        // Arrange
        
        var verificationToken = new VerificationTokenBuilder().Build();
        
        // Act
        
        verificationToken.UpdateLastSendOnUtc();
        
        // Assert
        
        verificationToken
            .LastSendOnUtc
            .Should()
            .BeCloseTo(
                DateTime.UtcNow,
                TimeSpan.FromMinutes(Constants.VerificationToken.MaxDifferenceInMinutes));
    }
}