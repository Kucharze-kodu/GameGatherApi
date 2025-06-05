namespace GameGather.Domain.Aggregates.Users.Enums;

public enum TokenStatus
{
    NotSent = 0,
    SentWaitingForResend = 1,
    SentAndReadyToResend = 2,
    Used = 3,
    Expired = 4,
}