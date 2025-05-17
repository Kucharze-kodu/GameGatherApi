namespace GameGather.Domain.Aggregates.Users.Enums;

public enum TokenStatus
{
    TokenExpired = 0,
    TokenAlreadySent = 1,
    TokenReadyToResend = 2,
    TokenUsed = 3,
    TokenRefresh = 4,
}