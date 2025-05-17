using GameGather.Application.Common.Messaging;

namespace GameGather.Application.Features.Users.Commands.VerifyUser;

public record VerifyUserCommand(
    string Email,
    string VerificationCode) : ICommand<string>;