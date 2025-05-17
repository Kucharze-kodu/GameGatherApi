namespace GameGather.Application.Contracts.Users;

public record ResendVerificationTokenResponse(
    string Message,
    TimeOnly? TimeToWait = null);