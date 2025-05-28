using GameGather.Application.Common.Messaging;
using GameGather.Application.Contracts.Users;

namespace GameGather.Application.Features.Users.Commands.ResendVerificationToken;

public record ResendVerificationTokenCommand(
    string Email,
    string VerifyEmailUrl) : ICommand<ResendVerificationTokenResponse>;