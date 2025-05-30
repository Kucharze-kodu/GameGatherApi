﻿using ErrorOr;


namespace GameGather.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class SessionGameList
        {
            public static Error IsNotAuthorized = Error.Conflict(
            code: "SessionGame.NotAuthorized",
            description: "Login to create sessio game");
        }
    }
}
