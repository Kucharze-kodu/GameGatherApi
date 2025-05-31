using FluentAssertions;
using GameGather.Application.Configurations;
using GameGather.Application.Features.Users.Commands.LoginUser;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Common.Errors;
using GameGather.UnitTests.Utils.Builders.LoginUser;
using GameGather.UnitTests.Utils.Builders.Users;
using Microsoft.Extensions.Options;
using Moq;

namespace GameGather.Application.UnitTests.Features.Users.Commands.LoginUser;

public class LoginUserCommandHandlerTests
{
    private readonly LoginUserCommandHandler _loginUserCommandHandler;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IOptions<PasswordOptions>> _passwordOptionsMock;
    
    public LoginUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtProviderMock = new Mock<IJwtProvider>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _passwordOptionsMock = new Mock<IOptions<PasswordOptions>>();

        var passwordOptions = new PasswordOptions
        {
            ExpiryInDays = 90 // Example value
        };
        
        _passwordOptionsMock.Setup(p => p.Value).Returns(passwordOptions);

        _loginUserCommandHandler = new LoginUserCommandHandler(
            _userRepositoryMock.Object,
            _jwtProviderMock.Object,
            _passwordHasherMock.Object,
            _passwordOptionsMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnErrorInvalidCredentials_WhenUserNotFound()
    {
        // Arrange   
        
        var command = new LoginUserCommandBuilder().Build();
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(value: null);
        
        // Act
        
        var result = await _loginUserCommandHandler
            .Handle(command, default);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.InvalidCredentials);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default),
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorInvalidCredentials_WhenWrongPassword()
    {
        // Arrange   
        
        var command = new LoginUserCommandBuilder().Build();
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder().Build());
        
        _passwordHasherMock
            .Setup(p => p.Verify(
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns(false);
        
        // Act
        
        var result = await _loginUserCommandHandler
            .Handle(command, default);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.InvalidCredentials);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default),
                Times.Once);
        _passwordHasherMock
            .Verify(r => r.Verify(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorNotVerified_WhenUserNotVerified()
    {
        // Arrange   
        
        var command = new LoginUserCommandBuilder().Build();
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .NotVerified()
                .Build());
        
        _passwordHasherMock
            .Setup(p => p.Verify(
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns(true);
        
        // Act
        
        var result = await _loginUserCommandHandler
            .Handle(command, default);
        
        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.NotVerified);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default),
                Times.Once);
        _passwordHasherMock
            .Verify(r => r.Verify(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorPasswordExpired_WhenPasswordExpired()
    {
        // Arrange   
        
        var command = new LoginUserCommandBuilder().Build();
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .WithExpiredPassword()
                .Build());
        
        _passwordHasherMock
            .Setup(p => p.Verify(
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns(true);
        
        // Act
        
        var result = await _loginUserCommandHandler
            .Handle(command, default);
        
        // Assert

        result
            .IsError
            .Should()
            .BeFalse();
        result
            .Value
            .Token
            .Should()
            .BeNullOrEmpty();
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default),
                Times.Once);
        _passwordHasherMock
            .Verify(r => r.Verify(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnLoginUserResponse_WhenSuccessful()
    {
        // Arrange   
        
        var command = new LoginUserCommandBuilder().Build();
        
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder().Build());
        
        _passwordHasherMock
            .Setup(p => p.Verify(
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Returns(true);

        var jwtBearerTokenMock = new Mock<IJwtBearerToken>();
        jwtBearerTokenMock
            .SetupGet(t => t.Token)
            .Returns("token");
        jwtBearerTokenMock
            .SetupGet(t => t.ExpiresOnUtc)
            .Returns(DateTime.UtcNow.AddHours(1));
        
        _jwtProviderMock
            .Setup(r => r.GenerateToken(It.IsAny<User>()))
            .Returns(jwtBearerTokenMock.Object);
        
        // Act
        
        var result = await _loginUserCommandHandler
            .Handle(command, default);
        
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
            .NotBeNull();
        result
            .Value
            .TokenExpiresOnUtc
            .Should()
            .NotBe(null);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default),
                Times.Once);
        _passwordHasherMock
            .Verify(p => p.Verify(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        _jwtProviderMock
            .Verify(j => j.GenerateToken(It.IsAny<User>()),
                Times.Once);
    }
}