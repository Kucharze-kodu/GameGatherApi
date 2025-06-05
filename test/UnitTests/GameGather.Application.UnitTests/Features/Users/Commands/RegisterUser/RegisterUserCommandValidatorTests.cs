using FluentValidation.TestHelper;
using GameGather.Application.Features.Users.Commands.RegisterUser;
using GameGather.UnitTests.Utils.Builders.ApplicationUsers.Commands;

namespace GameGather.Application.UnitTests.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandValidatorTests
{
    private readonly RegisterUserCommandValidator _registerUserCommandValidator = new();
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task FirstName_Should_NotBeEmpty(string firstName)
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithFirstName(firstName)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.FirstName);
    }
    
    [Fact]
    public async Task FirstName_Should_BeMaximum100CharactersLong()
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithFirstNameMinimum100CharactersLong()
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.FirstName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task LastName_Should_NotBeEmpty(string lastName)
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithLastName(lastName)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public async Task LastName_Should_BeMaximum100CharactersLong()
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithLastNameMinimum100CharactersLong()
            .Build();
        
        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.LastName);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task Email_Should_NotBeEmpty(string email)
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithEmail(email)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
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
        
        var command = new RegisterUserCommandBuilder()
            .WithEmail(email)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
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
        
        var command = new RegisterUserCommandBuilder()
            .WithPassword(password)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Password);
    }
    
    [Theory]
    [InlineData("a")]
    [InlineData("a2")]
    [InlineData("a2c")]
    [InlineData("a2c3")]
    [InlineData("a2c3d4")]
    [InlineData("1234567")]
    public async Task Password_Should_BeAtLeast8CharactersLong(string password)
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithPassword(password)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Password);
    }
    
    [Theory]
    [InlineData("test")]
    [InlineData("P4ssw0rd!")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task ConfirmPassword_Should_MatchPassword(string confirmPassword)
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithPassword("OtherPassword")
            .WithConfirmPassword(confirmPassword)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);

        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
    }
    
    [Theory]
    [InlineData(null)]
    public async Task Birthday_Should_NotBeEmpty(DateTime birthday)
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .WithBirthday(birthday)
            .Build();

        // Act
        
        var result = await _registerUserCommandValidator
            .TestValidateAsync(command);
        
        // Assert
        
        result
            .ShouldHaveValidationErrorFor(x => x.Birthday);
            
    }
    
}