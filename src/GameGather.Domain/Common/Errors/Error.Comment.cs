using ErrorOr;

namespace GameGather.Domain.Common.Errors
{
    public static partial class Errors
    { 
        public static class Comment
        {
            public static Error IsNotAuthorized = Error.Conflict(
                code: "Comment.NotAuthorized",
                description: "Login to create session game");

            public static Error IsWrongData = Error.Conflict(
                code: "comment.IsWrongData",
                description: "We dont have data");
        }
    }
}

