using System.Security.Claims;
using FluentAssertions;
using GameGather.Infrastructure.Utils;
using GameGather.Infrastructure.Utils.Extensions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace GameGather.Infrastructure.UnitTests.Utils;

public class UserContextTests
{
    private UserContext _userContext;
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;

    public UserContextTests()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _userContext = new UserContext(_httpContextAccessorMock.Object);
    }
    
    [Fact]
    public void IsAuthenticated_Should_ReturnNull_WhenHttpContextDoesNotExist()
    {
        // Arrange
        
        _httpContextAccessorMock
            .SetupGet(x => x
                .HttpContext)
            .Returns(value: null);
        
        // Act
        
        var result = _userContext.IsAuthenticated;
        
        // Assert
        
        result
            .Should()
            .BeNull();
    }
    
    [Fact]
    public void IsAuthenticated_Should_ReturnNull_WhenIdentityDoesNotExist()
    {
        // Arrange
        
        _httpContextAccessorMock
            .SetupGet(x => x
                .HttpContext
                .User
                .Identity)
            .Returns(value: null);
        
        // Act
        
        var result = _userContext.IsAuthenticated;
        
        // Assert
        
        result
            .Should()
            .BeNull();
    }
    
    [Fact]
    public void IsAuthenticated_Should_ReturnFalse_WhenUserIsNotAuthenticated()
    {
        // Arrange
        
        _httpContextAccessorMock
            .SetupGet(x => x
                .HttpContext
                .User
                .Identity
                .IsAuthenticated)
            .Returns(false);
        
        // Act
        
        var result = _userContext.IsAuthenticated;
        
        // Assert
        
        result
            .Should()
            .BeFalse();
    }
    
    [Fact]
    public void IsAuthenticated_Should_ReturnTrue_WhenUserIsAuthenticated()
    {
        // Arrange
        
        _httpContextAccessorMock
            .SetupGet(x => x
                .HttpContext
                .User
                .Identity
                .IsAuthenticated)
            .Returns(true);
        
        // Act
        
        var result = _userContext.IsAuthenticated;
        
        // Assert
        
        result
            .Should()
            .BeTrue();
    }
    
    [Fact]
    public void UserId_Should_ReturnNull_WhenHttpContextDoesNotExist()
    {
        // Arrange
        
        _httpContextAccessorMock
            .SetupGet(x => x
                .HttpContext)
            .Returns(value: null);
        
        // Act
        
        var result = _userContext.UserId;
        
        // Assert
        
        result
            .Should()
            .BeNull();
    }
    
    [Fact]
    public void UserId_Should_ReturnNull_WhenUserDoesNotExist()
    {
        // Arrange
        
        var mockHttpContext = new DefaultHttpContext();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1")
        }));
        mockHttpContext.User = claimsPrincipal;
        
        _httpContextAccessorMock
            .SetupGet(x => x
                .HttpContext)
            .Returns(mockHttpContext);
        
        // Act
        
        var result = _userContext.UserId;
        
        // Assert

        result
            .Should()
            .NotBeNull();
    }
}