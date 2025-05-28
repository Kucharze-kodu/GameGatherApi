using GameGather.Domain.Aggregates.Users.Enums;
using Password_ = GameGather.Domain.Aggregates.Users.ValueObjects.Password;

namespace GameGather.Domain.UnitTests.TestUtils.Constants.Users;

public static partial class Constants
{
    public static class User
    {
        public const int UserIdValue = 1;
        public const string FirstName = "John";
        public const string LastName = "Doe";
        public const string Email = "john.doe@gmail.com";
        public const string PasswordValue = Constants
            .Password
            .Value;
        public static readonly DateTime Birthday = DateTime
            .UtcNow
            .AddYears(-AgeOfUser);
        public static readonly DateTime CreatedOnUtc = DateTime
            .UtcNow
            .AddDays(-AccountAgeInDays);
        public static readonly DateTime? VerifiedOnUtc = DateTime
            .UtcNow
            .AddDays(-DaysSinceVerification);
        public static readonly DateTime LastModifiedOnUtc = DateTime
            .UtcNow
            .AddDays(-DaysSinceLastModification);
        public static readonly Guid VerificationTokenValue = Guid.NewGuid();
        public static readonly Guid ResetPasswordTokenValue = Guid.NewGuid();
        public static readonly Role Role = Role.User;
        public const string VerifyEmailUrl = "https://example.com/verify-email";
        
        
        private const int AgeOfUser = 20;
        private const int AccountAgeInDays = 30;
        private const int DaysSinceVerification = 7;
        private const int DaysSinceLastModification = 1;
    }
}