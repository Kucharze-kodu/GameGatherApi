using FluentAssertions;
using FluentValidation.TestHelper;
using GameGather.Application.Features.Users.Commands.LoginUser;
using GameGather.UnitTests.Utils.Builders.LoginUser;

namespace GameGather.Application.UnitTests.Features.Users.Commands.LoginUser;

public class LoginUserCommandValidatorTests
{
    private readonly LoginUserCommandValidator _loginUserCommandValidator = new();
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Email_Should_NotBeEmpty(string email)
    {
        // Arrange
        
        var command = new LoginUserCommandBuilder()
            .WithEmail(email)
            .Build();

        // Act
        
        var result = await _loginUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Theory]
    [InlineData("invalid-email")]
    [InlineData("invalid-email@")]
    [InlineData("@domain")]
    public async Task Email_Should_BeValid(string email)
    {
        // Arrange
        
        var command = new LoginUserCommandBuilder()
            .WithEmail(email)
            .Build();

        // Act
        
        var result = await _loginUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Password_Should_NotBeEmpty(string password)
    {
        // Arrange
        
        var command = new LoginUserCommandBuilder()
            .WithPassword(password)
            .Build();

        // Act
        
        var result = await _loginUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Password);
    }
}