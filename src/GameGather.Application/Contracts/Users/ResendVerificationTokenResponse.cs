using GameGather.Domain.Aggregates.Users.Enums;

namespace GameGather.Application.Contracts.Users;

public record ResendVerificationTokenResponse(
    TokenStatus Status,
    TimeSpan? TimeToWait = null);