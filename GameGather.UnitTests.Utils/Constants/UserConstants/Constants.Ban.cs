namespace GameGather.UnitTests.Utils;

public static partial class Constants
{
    public static class Ban
    {
        public static readonly DateTime CreatedOnUtc = DateTime.UtcNow;
        public static readonly DateTime ExpiresOnUtc = DateTime.UtcNow.AddDays(30);
        public const string Message = "Test ban message";
    }
}