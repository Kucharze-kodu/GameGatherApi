using FluentValidation.TestHelper;
using GameGather.Application.Features.Users.Commands.VerifyUser;
using GameGather.UnitTests.Utils.Builders.VerifyUser;

namespace GameGather.Application.UnitTests.Features.Users.Commands.VerifyUser;

public class VerifyUserCommandValidatorTests
{
    private readonly VerifyUserCommandValidator _verifyUserCommandValidator = new();
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Email_Should_NotBeEmpty(string email)
    {
        // Arrange
        
        var command = new VerifyUserCommandBuilder()
            .WithEmail(email)
            .Build();
        
        // Act
        
        var result = await _verifyUserCommandValidator
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
        
        var command = new VerifyUserCommandBuilder()
            .WithEmail(email)
            .Build();
        
        // Act
        
        var result = await _verifyUserCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task VerificationCode_Should_NotBeEmpty(string verificationCode)
    {
        // Arrange
        
        var command = new VerifyUserCommandBuilder()
            .WithVerificationCode(verificationCode)
            .Build();
        
        // Act
        
        var result = await _verifyUserCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.VerificationCode);
    }
    
    [Theory]
    [InlineData("invalid-code")]
    [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public async Task VerificationCode_Should_BeValidFormat(string verificationCode)
    {
        // Arrange
        
        var command = new VerifyUserCommandBuilder()
            .WithVerificationCode(verificationCode)
            .Build();
        
        // Act
        
        var result = await _verifyUserCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.VerificationCode);
    }
}