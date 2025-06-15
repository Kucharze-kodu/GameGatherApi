using GameGather.Domain.Aggregates.Users.Enums;

namespace GameGather.UnitTests.Utils;

public static partial class Constants
{
    public static class ResetPasswordToken
    {
        public static readonly Guid Value = Guid.NewGuid();
        public static readonly DateTime CreatedOnUtc = DateTime.UtcNow.AddHours(-12);
        public static readonly DateTime ExpiresOnUtc = DateTime.UtcNow.AddDays(12);
        public static readonly DateTime LastSendOnUtc = DateTime.UtcNow.AddHours(-2);
        public static readonly DateTime UsedOnUtc = DateTime.UtcNow.AddHours(-1);
        public const TokenType Type = TokenType.ResetPasswordToken;
        public const int TokenValidityInDays = 1;
        public const int MinimumTimeToResendInMinutes = 5;
        public const int MaxDifferenceInMinutes = 5;
    }
}