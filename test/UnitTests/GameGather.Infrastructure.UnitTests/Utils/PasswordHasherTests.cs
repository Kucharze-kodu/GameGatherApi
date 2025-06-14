using FluentAssertions;
using GameGather.Infrastructure.Utils;
using GameGather.UnitTests.Utils;

namespace GameGather.Infrastructure.UnitTests.Utils;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Fact]
    public void Hash_Should_Return_HashedPassword()
    {
        // Arrange

        var password = Constants.Password.Value;

        // Act
        
        var hashedPassword = _passwordHasher.Hash(password);

        // Assert
        
        hashedPassword
            .Should()
            .NotBeNullOrEmpty()
            .And
            .NotBeSameAs(password);
    }

    [Fact]
    public void Hash_Should_ReturnDifferentHashes_ForSamePassword()
    {
        // Arrange

        var password = Constants.Password.Value;

        // Act

        var hashedPassword1 = _passwordHasher.Hash(password);
        var hashedPassword2 = _passwordHasher.Hash(password);

        // Assert

        hashedPassword1
            .Should()
            .NotBeNullOrEmpty()
            .And
            .NotBeSameAs(hashedPassword2);
    }
    
    [Fact]
    public void Verify_Should_ReturnTrue_ForCorrectPassword()
    {
        // Arrange

        var password = Constants.Password.Value;
        var hashedPassword = _passwordHasher.Hash(password);

        // Act

        var isVerified = _passwordHasher.Verify(password, hashedPassword);

        // Assert

        isVerified
            .Should()
            .BeTrue();
    }
    
    [Fact]
    public void Verify_Should_ReturnFalse_ForIncorrectPassword()
    {
        // Arrange

        var password = Constants.Password.Value;
        var hashedPassword = _passwordHasher.Hash(password);
        var incorrectPassword = "WrongPassword";

        // Act

        var isVerified = _passwordHasher.Verify(incorrectPassword, hashedPassword);

        // Assert

        isVerified
            .Should()
            .BeFalse();
    }
}