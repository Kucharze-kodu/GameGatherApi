using ErrorOr;


namespace GameGather.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class SessionGame
        {
            public static Error IsNotAuthorized = Error.Conflict(
            code: "SessionGame.NotAuthorized",
            description: "Login to create sessio game");

            public static Error IsWrongData = Error.Conflict(
            code: "SessionGame.IsWrongData",
            description: "We dont have data");
        }
    }
}
