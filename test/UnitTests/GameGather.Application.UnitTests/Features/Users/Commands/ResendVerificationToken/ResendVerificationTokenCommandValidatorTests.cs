using FluentValidation.TestHelper;
using GameGather.Application.Features.Users.Commands.ResendVerificationToken;
using GameGather.UnitTests.Utils.Builders.ApplicationUsers.Commands;

namespace GameGather.Application.UnitTests.Features.Users.Commands.ResendVerificationToken;

public class ResendVerificationTokenCommandValidatorTests
{
    private readonly ResendVerificationTokenCommandValidator _resendVerificationTokenCommandValidator = new();
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Email_Should_NotBeEmpty(string email)
    {
        // Arrange
        
        var command = new ResendVerificationTokenCommandBuilder()
            .WithEmail(email)
            .Build();
        
        // Act
        
        var result = await _resendVerificationTokenCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Theory]
    [InlineData("invalid-email")]
    [InlineData("invalid-email@")]
    [InlineData("@domain")]
    public async Task Email_Should_BeValidEmailAddress(string email)
    {
        // Arrange
        
        var command = new ResendVerificationTokenCommandBuilder()
            .WithEmail(email)
            .Build();
        
        // Act
        
        var result = await _resendVerificationTokenCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task VerifyEmailUrl_Should_NotBeEmpty(string verifyEmailUrl)
    {
        // Arrange
        
        var command = new ResendVerificationTokenCommandBuilder()
            .WithVerifyEmailUrl(verifyEmailUrl)
            .Build();
        
        // Act
        
        var result = await _resendVerificationTokenCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.VerifyEmailUrl);
    }
}