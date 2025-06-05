using FluentAssertions;
using GameGather.Application.Features.Users.Events;
using GameGather.Application.Persistance;
using GameGather.Application.Utils.Email;
using GameGather.UnitTests.Utils.Builders.ApplicationUsers.Events;
using GameGather.UnitTests.Utils.Builders.DomainUsers;
using Moq;

namespace GameGather.Application.UnitTests.Features.Users.Events;

public class VerificationTokenRefreshedDomainEventHandlerTests
{
    private readonly VerificationTokenRefreshedDomainEventHandler _verificationTokenRefreshedDomainEventHandler;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public VerificationTokenRefreshedDomainEventHandlerTests()
    {
        _emailServiceMock = new Mock<IEmailService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _verificationTokenRefreshedDomainEventHandler = new VerificationTokenRefreshedDomainEventHandler(
            _emailServiceMock.Object,
            _userRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_WhenUserNotFound()
    {
        // Arrange

        var notification = new VerificationTokenRefreshedDomainEventBuilder()
            .Build();

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(value: null);

        // Act 

        var result = () => _verificationTokenRefreshedDomainEventHandler
            .Handle(notification, default);

        // Assert

        await result
            .Should()
            .ThrowAsync<InvalidOperationException>();
    }
    
    [Fact]
    public async Task Handle_Should_SendEmailWithVerificationTokenAsync_WhenUserFound()
    {
        // Arrange

        var notification = new VerificationTokenRefreshedDomainEventBuilder()
            .Build();

        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(
                It.IsAny<string>(),
                default))
            .ReturnsAsync(new UserBuilder()
                .NotVerified()
                .Build());

        // Act 

        await _verificationTokenRefreshedDomainEventHandler
            .Handle(notification, default);

        // Assert

        _emailServiceMock
            .Verify(x => x.SendEmailWithVerificationTokenAsync(
                    notification.Email,
                    notification.FirstName,
                    notification.VerificationToken.ToString(),
                    notification.VerifyEmailUrl,
                    default), 
                Times.Once);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(default), 
            Times.Once);
    }
    
}