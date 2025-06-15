namespace GameGather.IntegrationTests.TestUtils;

public static partial class Constants
{
    public static class LoginUser
    {
        public const string Email = "joe.doe@gmail.com";
        public const string Password = "P4ssw0rd123!";
    }

    public static class LoginUserWithExpiredPassword
    {
        public const string Email = "bob.rolin@gmail.com";
        public const string Password = "P4ssw0rd123!";
    }

    public static class LoginUserWithNotVerifiedEmail
    {
        public const string Email = "caroline.depr@gmail.com";
        public const string Password = "P4ssw0rd123!";
    }
}