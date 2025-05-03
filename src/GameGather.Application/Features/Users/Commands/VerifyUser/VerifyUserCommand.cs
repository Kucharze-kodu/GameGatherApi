using GameGather.Application.Common.Messaging;

namespace GameGather.Application.Features.Users.Commands.VerifyUser;

public record VerifyUserCommand(
    int Id,
    Guid Token) : ICommand<string>;