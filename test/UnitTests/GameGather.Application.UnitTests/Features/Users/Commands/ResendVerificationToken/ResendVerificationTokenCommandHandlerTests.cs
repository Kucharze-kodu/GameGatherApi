using FluentAssertions;
using GameGather.Application.Features.Users.Commands.ResendVerificationToken;
using GameGather.Application.Persistance;
using GameGather.Application.Utils.Email;
using GameGather.Domain.Aggregates.Users.Enums;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.Domain.Common.Errors;
using GameGather.UnitTests.Utils;
using GameGather.UnitTests.Utils.Builders.ApplicationUsers.Commands;
using GameGather.UnitTests.Utils.Builders.DomainUsers;
using Moq;

namespace GameGather.Application.UnitTests.Features.Users.Commands.ResendVerificationToken;

public class ResendVerificationTokenCommandHandlerTests
{
    private readonly ResendVerificationTokenCommandHandler _resendVerificationTokenCommandHandler;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IEmailService> _emailServiceMock;

    public ResendVerificationTokenCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _emailServiceMock = new Mock<IEmailService>();
        _resendVerificationTokenCommandHandler = new ResendVerificationTokenCommandHandler(
            _userRepositoryMock.Object, 
            _unitOfWorkMock.Object, 
            _emailServiceMock.Object);

        _emailServiceMock
            .Setup(r => r.SendEmailWithVerificationTokenAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                default));
    }

    [Fact]
    public async Task Handle_Should_ReturnUserNotFoundError_WhenUserNotFound()
    {
        // Arrange

        var command = new ResendVerificationTokenCommandBuilder()
            .Build();

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(value: null);
        
        // Act
        
        var result = await _resendVerificationTokenCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.User.NotFound);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default), 
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnErrorTokenInvalid_WhenTokenIsUsed()
    {
        // Arrange

        var command = new ResendVerificationTokenCommandBuilder()
            .Build();

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .WithVerificationToken(new VerificationTokenBuilder()
                    .WithUsedToken()
                    .Build())
                .Build());
        
        // Act
        
        var result = await _resendVerificationTokenCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .IsError
            .Should()
            .BeTrue();
        result
            .Errors
            .Should()
            .Contain(Errors.Token.Invalid);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default), 
                Times.Once);
    }

    [Fact]
    public async Task Handle_Should_GenerateNewVerificationTokenAndReturnTokenStatusExpired_WhenTokenIsExpired()
    {
        // Arrange
        
        var command = new ResendVerificationTokenCommandBuilder()
            .Build();
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .WithVerificationToken(new VerificationTokenBuilder()
                    .WithNotUsedOnUtc()
                    .WithExpiredToken()
                    .Build())
                .Build());
        
        // Act
        
        var result = await _resendVerificationTokenCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .Value
            .Status
            .Should()
            .Be(TokenStatus.Expired);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default), 
                Times.Once);
        _unitOfWorkMock
            .Verify(r => r.SaveChangesAsync(default),
                Times.Once);
    }
    
    [Fact]
    public async Task Handle_Should_SendEmailWithVerificationTokenAndReturnTokenStatusNotSent_WhenTokenIsNotSentYet()
    {
        // Arrange
        
        var command = new ResendVerificationTokenCommandBuilder()
            .Build();
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .WithVerificationToken(new VerificationTokenBuilder()
                    .WithNotUsedOnUtc()
                    .WithNotLastSendOnUtc()
                    .Build())
                .Build());
        
        // Act
        
        var result = await _resendVerificationTokenCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .Value
            .Status
            .Should()
            .Be(TokenStatus.NotSent);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default), 
                Times.Once);
        _emailServiceMock
            .Verify(r => r.SendEmailWithVerificationTokenAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                default), 
                Times.Once);
        _unitOfWorkMock
            .Verify(r => r.SaveChangesAsync(default),
                Times.Once);
    }

    [Fact]
    public async Task
        Handle_Should_SendEmailWithVerificationTokenAndReturnTokenStatusSentAndReadyToResend_WhenTokenIsReadyToResend()
    {
        // Arrange
        
        var command = new ResendVerificationTokenCommandBuilder()
            .Build();
        var timeAfterLastSend = DateTime.UtcNow.AddMinutes(
            -Constants.VerificationToken.MinimumTimeToResendInMinutes - 1);
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .WithVerificationToken(new VerificationTokenBuilder()
                    .WithNotUsedOnUtc()
                    .WithLastSendOnUtc(timeAfterLastSend)
                    .Build())
                .Build());
        
        // Act
        
        var result = await _resendVerificationTokenCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .Value
            .Status
            .Should()
            .Be(TokenStatus.SentAndReadyToResend);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default),
                Times.Once);
        _emailServiceMock
            .Verify(r => r.SendEmailWithVerificationTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    default),
                Times.Once);
        _unitOfWorkMock
            .Verify(r => r.SaveChangesAsync(default),
                Times.Once);

    }
    
    [Fact]
    public async Task Handle_Should_ReturnTokenStatusSentWaitingForResend_WhenTokenIsNotReadyToResend()
    {
        // Arrange

        var command = new ResendVerificationTokenCommandBuilder()
            .Build();
        var timeAfterLastSend = DateTime.UtcNow.AddMinutes(
            -Constants.VerificationToken.MinimumTimeToResendInMinutes + 1);
        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .WithVerificationToken(new VerificationTokenBuilder()
                    .WithNotUsedOnUtc()
                    .WithLastSendOnUtc(timeAfterLastSend)
                    .Build())
                .Build());
        
        // Act
        
        var result = await _resendVerificationTokenCommandHandler
            .Handle(command, default);
        
        // Assert
        
        result
            .Value
            .Status
            .Should()
            .Be(TokenStatus.SentWaitingForResend);
        _userRepositoryMock
            .Verify(r => r.GetByEmailAsync(
                    It.IsAny<string>(),
                    default), 
                Times.Once);
        _emailServiceMock
            .Verify(r => r.SendEmailWithVerificationTokenAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                default), 
                Times.Never);
        _unitOfWorkMock
            .Verify(r => r.SaveChangesAsync(default),
                Times.Never);
    }
    
}