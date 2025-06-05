using FluentAssertions;
using GameGather.Application.Features.Users.Commands.RegisterUser;
using GameGather.Application.Persistance;
using GameGather.Application.Utils;
using GameGather.Domain.Aggregates.Users;
using GameGather.Domain.Common.Errors;
using GameGather.UnitTests.Utils.Builders.ApplicationUsers.Commands;
using Moq;

namespace GameGather.Application.UnitTests.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandlerTests
{
    private readonly RegisterUserCommandHandler _registerUserCommandHandler;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;

    public RegisterUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _registerUserCommandHandler = new RegisterUserCommandHandler(
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _passwordHasherMock.Object);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorDuplicateEmail_WhenEmailAlreadyExists()
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .Build();
        
        _userRepositoryMock
            .Setup(r => r.AnyUserAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(true);

        // Act
        
        var result = await _registerUserCommandHandler
            .Handle(command, default);

        // Assert

        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.DuplicateEmail);
        _userRepositoryMock
            .Verify(r => r.AnyUserAsync(
                    It.IsAny<string>(), 
                    default), 
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserRegisteredSuccessfully()
    {
        // Arrange
        
        var command = new RegisterUserCommandBuilder()
            .Build();
        
        _userRepositoryMock
            .Setup(r => r.AnyUserAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(false);
        
        _passwordHasherMock
            .Setup(p => p.Hash(It.IsAny<string>()))
            .Returns("hashedPassword");

        // Act
        
        var result = await _registerUserCommandHandler
            .Handle(command, default);

        // Assert

        result
            .IsError
            .Should()
            .BeFalse();
        result
            .Value
            .Message
            .Should()
            .NotBeNullOrEmpty();
        
        _userRepositoryMock
            .Verify(r => r.AddUserAsync(
                    It.IsAny<User>(), 
                    default), 
                Times.Once);
        
        _unitOfWorkMock
            .Verify(r => r
                    .SaveChangesAsync(default), Times.Once);
    }
    
    
}