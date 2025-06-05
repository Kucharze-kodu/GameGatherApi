using FluentAssertions;
using GameGather.Application.Features.Users.Commands.VerifyUser;
using GameGather.Application.Persistance;
using GameGather.Domain.Common.Errors;
using GameGather.UnitTests.Utils.Builders.Users;
using GameGather.UnitTests.Utils.Builders.VerifyUser;
using Moq;

namespace GameGather.Application.UnitTests.Features.Users.Commands.VerifyUser;

public class VerifyUserCommandHandlerTests
{
    private readonly VerifyUserCommandHandler _verifyUserCommandHandler;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public VerifyUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _verifyUserCommandHandler = new VerifyUserCommandHandler(
            _userRepositoryMock.Object, 
            _unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnUserNotFoundError_WhenUserNotFound()
    {
        // Arrange
        
        var command = new VerifyUserCommandBuilder()
            .Build();

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(value: null);
        
        // Act
        
        var result = await _verifyUserCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .ContainSingle(e => e == Errors.User.NotFound);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnInvalidTokenError_WhenTokenIsInvalidOrExpiredOrUsed()
    {
        // Arrange
        
        var command = new VerifyUserCommandBuilder()
            .WithVerificationCode(Guid.NewGuid().ToString())
            .Build();

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .Build());
        
        // Act
        
        var result = await _verifyUserCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .ContainSingle(e => e == Errors.User.InvalidToken);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenVerificationTokenIsValid()
    {
        // Arrange
        
        var command = new VerifyUserCommandBuilder()
            .Build();

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .NotVerified()
                .Build());
        
        // Act
        
        var result = await _verifyUserCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .IsError
            .Should()
            .BeFalse();
        result
            .Value
            .Should()
            .NotBeNullOrEmpty();
        _unitOfWorkMock
            .Verify(r => r.SaveChangesAsync(default), 
                Times.Once);
    }
}