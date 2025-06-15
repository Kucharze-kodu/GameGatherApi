using ErrorOr;
using FluentAssertions;
using GameGather.Domain.Common.Errors;
using GameGather.IntegrationTests.Abstractions;
using GameGather.IntegrationTests.Users.TestUtils;
using static GameGather.IntegrationTests.Users.TestUtils.LoginUserCommandBuilder;

namespace GameGather.IntegrationTests.Users;

public class LoginUserTests : BaseIntegrationTest
{
    public LoginUserTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorInvalidCredentials_WhenUserNotFound()
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .WithEmail("random_email@gmail.com")
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.InvalidCredentials);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorInvalidCredentials_WhenWrongPassword()
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .WithPassword("wrong_password")
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.InvalidCredentials);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorNotVerified_WhenUserNotVerified()
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .WithNotVerifiedEmail()
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.NotVerified);
    }
    
    [Fact]
    public async Task Handle_Should_NotReturnToken_WhenPasswordExpired()
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .WithExpiredPassword()
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeFalse();
        result
            .Value
            .Token
            .Should()
            .BeNull();
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid-email")]
    [InlineData("invalid-email@")]
    [InlineData("@domain")]
    public async Task Handle_Should_ReturnValidationError_WhenEmailIsInvalid(string email)
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .WithEmail(email)
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(error => error.Type == ErrorType.Validation);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Handle_Should_ReturnValidationError_WhenPasswordIsInvalid(string password)
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .WithPassword(password)
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(error => error.Type == ErrorType.Validation);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnLoginUserResponse_WhenUserIsValid()
    {
        // Arrange   
        
        var command = GivenLoginUserCommand()
            .Build();
        
        // Act
        
        var result = await Sender
            .Send(command);
        
        // Assert

        result
            .IsError
            .Should()
            .BeFalse();
        result
            .Value
            .Should()
            .NotBeNull();
        result
            .Value
            .Token
            .Should()
            .NotBeNullOrEmpty();
    }
}