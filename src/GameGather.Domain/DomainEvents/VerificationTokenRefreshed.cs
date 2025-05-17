using GameGather.Domain.Common.Primitives;

namespace GameGather.Domain.DomainEvents;

public record VerificationTokenRefreshed(
    string FirstName,
    string Email,
    Guid VerificationToken,
    string VerifyEmailUrl) : IDomainEvent;