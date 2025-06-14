using ErrorOr;


namespace GameGather.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class PostGame
        {
            public static Error IsNotAuthorized = Error.Conflict(
                code: "PostGame.NotAuthorized",
                description: "Login to create session game");

            public static Error IsWrongData = Error.Conflict(
                code: "PostGame.IsWrongData",
                description: "We dont have data");
        }
    }
}
