using FluentAssertions;
using GameGather.Domain.Aggregates.Users.ValueObjects;
using GameGather.UnitTests.Utils.Builders.DomainUsers;

namespace GameGather.Domain.UnitTests.Aggregates.Users.ValueObjects;

public class BanTests
{
    [Fact]
    public void Create_Ban_ShouldReturnBan()
    {
        // Arrange
        var templateBan = new BanBuilder().Build();

        // Act
        var ban = Ban.Create(
            templateBan.Message,
            templateBan.ExpiresOnUtc
        );

        // Assert
        ban.Should().NotBeNull();
        ban.CreatedOnUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(5));
        ban.Message.Should().Be(templateBan.Message);
        ban.ExpiresOnUtc.Should().Be(templateBan.ExpiresOnUtc);
    }
    
    [Fact]
    public void Load_Ban_ShouldReturnLoadedBan()
    {
        // Arrange
        var banToLoad = new BanBuilder().Build();
        var ban = Ban.Create();
    
        // Act
        ban.Load(
            banToLoad.CreatedOnUtc,
            banToLoad.ExpiresOnUtc,
            banToLoad.Message
        );
    
        // Assert
        ban.Should().NotBeNull();
        ban.CreatedOnUtc.Should().Be(banToLoad.CreatedOnUtc);
        ban.Message.Should().Be(banToLoad.Message);
        ban.ExpiresOnUtc.Should().Be(banToLoad.ExpiresOnUtc);
    }
}